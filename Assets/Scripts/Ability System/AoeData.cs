public enum CenterType
{
    Caster,
    Target,
    Point,
    Projectile,
    Attacker,
}

public enum ShapeType
{
    Circle,
    Rectangle,
    Arc
}


[System.Serializable]
public class AoeData
{
    public CenterType Center;
    public ShapeType Shape;

    // circle shape
    public int Radius;

    // rect shape
    public int Width;
    public int Distance;

    public int MaxTargets = -1;
}