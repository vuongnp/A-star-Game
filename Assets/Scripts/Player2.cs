using Completed;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player2 : MovingObject
{    
    private Rigidbody2D r2;
    private Animator animator;                   
    private bool flag = true;
    bool flag2 = false;
    private GameObject axe;
    public GameObject item;
    private GameObject[] wall;
    private GameObject[] fire;
    private GameObject[] floor;
    private List<Vector3> gridPositions = new List<Vector3>();
    SquareGrid graph;
    public Dictionary<Location, int> costObject = new Dictionary<Location, int>();
    List<Location> path;
    private float deltaT;
    Location start, goal;
    AStarSearch2 astar;

    public void initMap()
    {
        gridPositions.Clear();
        for (int x = 2; x < 13; x++)
        {
            for (int y = 2; y < 13; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
        int len = wall.Length;
        for (int i = 0; i < len; i++)
        {
            Vector3 tmp = new Vector3(wall[i].transform.position.x, wall[i].transform.position.y, 0f);
            gridPositions.Remove(tmp);
        }
        int len2 = fire.Length;
        if (len2 > 0)
        {
            for (int i = 0; i < len2; i++)
            {
                Vector3 tmp = new Vector3(fire[i].transform.position.x, fire[i].transform.position.y, 0f);
                gridPositions.Remove(tmp);
            }
        }
    }
    public Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    public void createCost()
    {
        int len = wall.Length;
        for (int i = 0; i < len; i++)
        {
            Location tmp = new Location(wall[i].transform.position.x, wall[i].transform.position.y);
            if (costObject.ContainsKey(tmp) == false)
                costObject.Add(tmp, System.Int32.MaxValue);
        }
        int lenFloor = floor.Length;
        for (int i = 0; i < lenFloor; i++)
        {
            Location tmp = new Location(floor[i].transform.position.x, floor[i].transform.position.y);
            if (costObject.ContainsKey(tmp) == false)
                costObject.Add(tmp, 1);
        }
        int lenFire = fire.Length;
        if (lenFire > 0)
        {
            for (int i = 0; i < lenFire; i++)
            {
                Location tmp = new Location(fire[i].transform.position.x, fire[i].transform.position.y);
                costObject.Remove(tmp);
                costObject.Add(tmp, 5);
            }
        }
    }
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        r2 = GetComponent<Rigidbody2D>();
        r2.gravityScale = 0;
        r2.rotation = 0;
		ScoreScipt2.scoreValue2=0;

        wall = GameObject.FindGameObjectsWithTag("Wall");
        floor = GameObject.FindGameObjectsWithTag("Floor");
        fire = GameObject.FindGameObjectsWithTag("Fire");       
        this.createCost();
        graph = new SquareGrid(1, 1, 14, 14, costObject);
        initMap();
        Instantiate(item, RandomPosition(), Quaternion.identity);
        axe = GameObject.FindGameObjectWithTag("Axe");
        start = new Location(transform.position.x, transform.position.y);
        goal = new Location(axe.transform.position.x, axe.transform.position.y);
        astar = new AStarSearch2(graph, start, goal, costObject);
        path = astar.FindPath();
        deltaT = 1.0f * astar.dem / 80;
        base.Start();

    }


    int i = 0;
    private void Tick()
    {      
        Vector2 movement = new Vector2();
        if (i >= path.Count) return;
        if (astar.dem < 30)
            astar.dem += 20;
        deltaT = 1.0f * astar.dem / 80;
        Location p = path[i++];
        if (p.x > transform.position.x)
            movement = new Vector2(p.x, 0);
        else if (p.x < transform.position.x)
            movement = new Vector2(-p.x, 0);
        if (p.y > transform.position.y)
            movement = new Vector2(0, p.y);
        else if (p.y < transform.position.y)
            movement = new Vector2(0, -p.y);
        movement = movement.normalized;

        transform.Translate(movement);
        
    }

    private float time = 0;
    private void Update()
    {
        if (flag == true)
        {
            if (flag2 == true)
            {
                flag2 = false;
                axe = GameObject.FindGameObjectWithTag("Axe");
                fire = GameObject.FindGameObjectsWithTag("Fire");
                start = new Location(transform.position.x, transform.position.y);
                goal = new Location(axe.transform.position.x, axe.transform.position.y);
                this.createCost();
                graph = new SquareGrid(1, 1, 14, 14, costObject);
                astar = new AStarSearch2(graph, start, goal, costObject);
                path = astar.FindPath();
                i = 0;
            }
            time += Time.deltaTime;
            if (time > deltaT)
            {

                Tick();
                time = 0;
                flag2 = true;
            }
        }
        if (flag == false)
        {
            axe = GameObject.FindGameObjectWithTag("Axe");
            if (axe == null)
            {
                fire = GameObject.FindGameObjectsWithTag("Fire");
                initMap();
                Instantiate(item, RandomPosition(), Quaternion.identity);
            }
                flag = true;
                flag2 = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.tag == "Axe")
        {
			ScoreScipt2.scoreValue2+=20;
            other.gameObject.SetActive(false);
            animator.SetTrigger("player2Ready");
            flag = false;
        }
        else if (other.tag == "Fire")
        {
            if (ScoreScipt2.scoreValue2 != 0)
                ScoreScipt2.scoreValue2 -= 10;
        }
    }
   
}
