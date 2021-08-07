using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FloatDamgeText.Create(new Vector3(0,4,-11),1000,DamageType.Physical);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
