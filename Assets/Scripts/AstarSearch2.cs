using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AStarSearch2
{
    
    public Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
    public Dictionary<Location, float> costSoFar = new Dictionary<Location, float>();
    private Location start;
    private Location goal;
    public int dem = 0;
    static public float Heuristic(Location a, Location b)
    {
        return 1.001f * ((Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y)));
    }

    public AStarSearch2(SquareGrid graph, Location start, Location goal, Dictionary<Location, int> costObject)
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
            Location parent = cameFrom[current];
            if (current.Equals(goal)) break;
            foreach (var neighbor in graph.Neighbors(current))
            {
                float newCost = costSoFar[current] + graph.Cost(current, neighbor);
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    if (costSoFar.ContainsKey(neighbor))
                    {
                        costSoFar.Remove(neighbor);
                        cameFrom.Remove(neighbor);
                    }

                    costSoFar.Add(neighbor, newCost);
                    cameFrom.Add(neighbor, current);
                    float priority = newCost + Heuristic(neighbor, goal);
                    frontier.Enqueue(neighbor, priority);
                    dem++;
                }
            }
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