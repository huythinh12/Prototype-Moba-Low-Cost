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
    public GameObject Enemy;
    public GameObject Ally;
    public GameObject Red;
    public GameObject Blue;
    public GameObject MinionMonster;


  
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
                    Instantiate(Enemy, legion.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Ally, legion.transform.position, Quaternion.identity);
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
                else
                {
                    Instantiate(MinionMonster, monster.transform.position, Quaternion.identity);

                }
            }

        }
    }
}

