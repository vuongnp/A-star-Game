using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid
{
    public static readonly Location[] DIRS = new[] {
    new Location(1, 0), 
    new Location(0, -1), 
    new Location(-1, 0), 
    new Location(0, 1), 
  };

    public SquareGrid(float x, float y, float width, float height, Dictionary<Location, int> costObject)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.costObject = costObject;
    }
    public float x, y, width, height;
    public Dictionary<Location, int> costObject = new Dictionary<Location, int>();
   // kiểm tra nằm trong map
    public bool InBounds(Location id)
    {
        return (x <= id.x) && (id.x < width) && (y <= id.y) && (id.y < height);
    }
    // kiểm tra có phải Wall hay không
    public bool Passable(Location id)
    {
        return (int)costObject[id] < System.Int32.MaxValue;
    }
    // trả về chi phí nút hàng xóm
    public float Cost(Location a, Location b)
    {
        return (float)(int)costObject[b];
    }
    // tập các hàng xóm của nút hiện tại
    public IEnumerable<Location> Neighbors(Location id)
    {
        foreach (var dir in DIRS)
        {
            Location next = new Location(id.x + dir.x, id.y + dir.y);
            if (InBounds(next) && Passable(next))
            {
                yield return next;
            }
        }
    }
}
