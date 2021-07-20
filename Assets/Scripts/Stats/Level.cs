using UnityEngine;
using System;

[Serializable]
public class Level
{
    public static readonly int Min = 1;
    public static readonly int Max = 15;

    [SerializeField, Min(1)] int start;
    private int current;


    public int Start
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
            else if (value > Max)
            {
                start = Max;
            }
            else
            {
                start = value;
            }
        }
    }

    public int Current
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


    public void LevelUp()
    {
        current++;
    }
}
