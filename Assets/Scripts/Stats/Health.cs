using UnityEngine;
using System;

[Serializable]
public class Health
{
    public static readonly float Min = 0;

    [SerializeField, Min(0)] float start;
    [SerializeField, Min(0)] float perLevel;
    float current;
    float max;


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

    public void Reset(int level)
    {
        SetMax(level);
        Current = Max;
    }
}
