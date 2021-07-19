using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Mana
{
    public static readonly float Min = 0;

    private float start;
    private float perLevel;
    private float current;
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
                current = Min;
            }
            else if (value > max)
            {
                current = max;
            }
            else
            {
                current = value;
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


    public void SetMax(int level)
    {
        Max = Start + PerLevel * level;
    }

    public void Healing(int amount)
    {
        Current += amount;
    }
}
