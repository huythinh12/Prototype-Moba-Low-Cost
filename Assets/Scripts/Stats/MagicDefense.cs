using UnityEngine;
using System;

[Serializable]
public class MagicDefense
{
    public static readonly float Min = 0;

    [SerializeField, Min(0)] float start;
    [SerializeField, Min(0)] float perLevel;
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
                current = Min;
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
