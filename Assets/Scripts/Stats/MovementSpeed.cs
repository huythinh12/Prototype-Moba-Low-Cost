using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MovementSpeed
{
    public static readonly float Min = 0;
    public static readonly float Max = 100;

    private float start;
    private float perLevel;
    private float current;
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

    public float Current
    {
        get
        {
            return current;
        }

        set
        {
            if (value < Min)
            {
                current = 0;
            }
            else if (value > Max)
            {
                current = Max;
            }
            else
            {
                current = value;
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

    public void SetCurrent(int level)
    {
        Current = Start + PerLevel * level + Temporary;
    }
}
