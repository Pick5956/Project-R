using UnityEngine;

public class Line
{
    Orientation orientation;
    Vector2Int coord;

    public Line(Orientation orientation,Vector2Int coord)
    {
        this.orientation = orientation;
        this.coord = coord;
    }

    public Orientation Orientation { get => orientation; set => orientation = value; }
    public Vector2Int Coord { get => coord; set => coord = value; }
}

public enum Orientation
{
    Horiszontal = 0,
    Vertical = 1,
}