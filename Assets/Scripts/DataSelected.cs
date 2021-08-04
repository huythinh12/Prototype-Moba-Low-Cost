using System.Collections.Generic;
using UnityEngine;

public class DataSelected : MonoBehaviour
{
    public static DataSelected Instance { get; private set; }
    public List<string> nameHero = new List<string>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

   
}
