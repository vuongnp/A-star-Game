using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location
{

    public readonly int x, y;

    public Location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Location(float x, float y)
    {
        this.x = Mathf.FloorToInt(x);
        this.y = Mathf.FloorToInt(y);
    }

    public Location(Vector3 position)
    {
        this.x = Mathf.RoundToInt(position.x);
        this.y = Mathf.RoundToInt(position.y);
    }

    public Vector3 vector3()
    {
        return new Vector3(this.x, this.y, 0f);
    }

    public override bool Equals(System.Object obj)
    {
        Location loc = obj as Location;
        return this.x == loc.x && this.y == loc.y;
    }

    public override int GetHashCode()
    {
        return (x * 597) ^ (y * 1173);
    }
}
