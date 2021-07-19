using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PhysicalDamage : MonoBehaviour
{
    public static float Min = 0;

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
            now = value;
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
