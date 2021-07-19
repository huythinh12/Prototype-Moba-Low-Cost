using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Mana
{
    public static float Min = 0;

    private float start;
    private float perLevel;
    private float now;
    private float max;


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
            if (value > max)
            {
                now = max;
            }
            else if (value < Min)
            {
                now = Min;
            }
            else
            {
                now = value;
            }
        }
    }

    public float Max
    {
        get
        {
            return max;
        }

        set
        {
            if (value < Min)
            {
                max = Min;
            }
            else
            {
                max = value;
            }
        }
    }
}
