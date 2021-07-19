using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Level
{
    const int minLevel = 1;

    private int start;
    private int now;
    private int max;


    public int Start
    {
        get
        {
            return start;
        }

        set
        {
            if (value < minLevel)
            {
                start = minLevel;
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

    public int Now
    {
        get
        {
            return now;
        }

        set
        {
            if (value < minLevel)
            {
                now = minLevel;
            }
            else if (value > max)
            {
                now = max;
            }
            else
            {
                now = value;
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
            max = value;
        }
    }


    public void LevelUp()
    {
        Now++;
    }
}
