using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Level
{
    public static readonly int Min = 1;

    private int start;
    private int current;
    private int max;


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
            else if (value > max)
            {
                start = max;
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

    public int Max
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


    public void LevelUp()
    {
        current++;
    }
}
