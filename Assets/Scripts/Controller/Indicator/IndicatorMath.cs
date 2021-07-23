using UnityEngine;

static public class JoystickMath
{
    static public float Angle360(Vector3 incaditor)
    {
        float angle180 = Vector3.Angle(Vector3.up, incaditor);
        return incaditor.x >= 0 ? angle180 : 360 - angle180;
    }

    static public Vector3 ConvertToOxzIndicator(Vector3 indicatorXY)
    {
        return new Vector3(indicatorXY.x, 0, indicatorXY.y);
    }

    static public Vector3 OxzIndicatorNormalized(Vector3 indicatorXZ)
    {
        return indicatorXZ.normalized;
    }

    static public Vector3 OxzIndicatorHalfNormalized(Vector3 indicatorXZ)
    {
        return OxzIndicatorNormalized(indicatorXZ) / 2f;
    }
}
