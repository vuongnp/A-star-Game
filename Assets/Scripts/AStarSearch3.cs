using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AStarSearch3
{
    public Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
    public Dictionary<Location, float> costSoFar = new Dictionary<Location, float>();
    private Location start;
    private Location goal;
    public int dem = 0;
    public int d = 0;
    static public float Heuristic(Location a, Location b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
    }


    public AStarSearch3(SquareGrid graph, Location start, Location goal, Dictionary<Location, int> costObject)
    {
        this.start = start;
        this.goal = goal;
        var frontier = new PriorityQueue<Location>();
        frontier.Enqueue(start, 0f);

        cameFrom.Add(start, start);
        costSoFar.Add(start, 0f);

        while (frontier.Count > 0f)
        {
            Location current = frontier.Dequeue();
            if (current.Equals(goal)) break;
            foreach (var neighbor in graph.Neighbors(current))
            {
                float newCost = costSoFar[current] + graph.Cost(current, neighbor);              
      
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {

                    // If we're replacing the previous cost, remove it
                    if (costSoFar.ContainsKey(neighbor))
                    {
                        costSoFar.Remove(neighbor);
                        cameFrom.Remove(neighbor);
                    }

                    costSoFar.Add(neighbor, newCost);
                    cameFrom.Add(neighbor, current);
                    float priority;
                    int N = (int)Heuristic(start, goal);
                    if (d <= N)
                        priority = newCost + 1.0f * (1 + 1.01f * (1 - 1.0f*d / N)) * Heuristic(neighbor, goal);
                    else
                        priority = newCost + Heuristic(neighbor, goal);
                    frontier.Enqueue(neighbor, priority);
                    dem++;
                }
            }
            d++;
        }

    }

    public List<Location> FindPath()
    {

        List<Location> path = new List<Location>();
        Location current = goal;

        while (!current.Equals(start))
        {
            if (!cameFrom.ContainsKey(current))
            {
                MonoBehaviour.print("cameFrom does not contain current.");
                return new List<Location>();
            }
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }
}