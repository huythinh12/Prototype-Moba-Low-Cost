using System.Diagnostics;
using UnityEngine;

public class SpawnLegion : MonoBehaviour
{
    [SerializeField]
    private Transform[] legions;
    [SerializeField]
    private Transform[] monsters;
    private Stopwatch timer;

    public GameObject Dragon;
    public GameObject[] LegionEnemy;
    public GameObject[] LegionAlly;
    public GameObject Red;
    public GameObject Blue;
    public GameObject MinionMonster;
    public GameObject RightMonster;
    public GameObject LeftMonster;


  
    // Start is called before the first frame update
    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        InvokeRepeating("SpawnBaseOnCondition", 1,1);
    }
    // Update is called once per frame
    void Update()
    {
        //SpawnBaseOnCondition();

    }

    private void OnDisable()
    {
        timer.Stop();
    }

    private void SpawnBaseOnCondition()
    {
        if ((int)timer.Elapsed.TotalSeconds % 3 == 0)
        {
            foreach (var legion in legions)
            {
                if (legion.transform.name == "SpawnEnemy")
                {
                    foreach (var item in LegionEnemy)
                    {
                    Instantiate(item, legion.transform.position, Quaternion.identity);

                    }
                }
                else
                {
                    foreach (var item in LegionAlly)
                    {
                        Instantiate(item, legion.transform.position, Quaternion.identity);

                    }
                }
            }
        }
        else if ((int)timer.Elapsed.TotalSeconds % 4 == 0)
        {
            foreach (var monster in monsters)
            {

                if (monster.name == "Dragon")
                {

                    Instantiate(Dragon, monster.transform.position, Quaternion.identity);
                }
                else if (monster.name == "Blue")
                {
                    Instantiate(Blue, monster.transform.position, Quaternion.identity);

                }
                else if (monster.name == "Red")
                {
                    Instantiate(Red, monster.transform.position, Quaternion.identity);

                }
                else if(monster.name.StartsWith("MinionRight"))
                {
                    Instantiate(RightMonster, monster.transform.position, Quaternion.identity);

                }
                else if (monster.name.StartsWith("MinionLeft"))
                {
                    Instantiate(LeftMonster, monster.transform.position, Quaternion.identity);

                }
                else
                {
                    Instantiate(MinionMonster, monster.transform.position, Quaternion.identity);

                }
            }

        }
    }
}

