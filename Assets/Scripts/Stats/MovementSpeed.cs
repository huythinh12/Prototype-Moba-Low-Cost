using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MovementSpeed
{
    public static float Min = 0;
    public static float Max = 100;

    private float start;
    private float perLevel;
    private float now;
    private float temporary;


    public float Start
    {
        get
        {
            return start;
        }

        set
        {
            if (value < Min)
            {
                start = Min;
            }
            else
            {
                start = value;
            }
        }
    }

    public float PerLevel
    {
        get
        {
            return perLevel;
        }

        set
        {
            perLevel = value;
        }
    }

    public float Now
    {
        get
        {
            return now;
        }

        set
        {
            if (value > Max)
            {
                now = Max;
            }
            else if (value < 0)
            {
                now = 0;
            }
            else
            {
                now = value;
            }
        }
    }

    public float Temporary
    {
        get
        {
            return temporary;
        }

        set
        {
            temporary = value;
        }
    }
}
