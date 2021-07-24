using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;


public class SpawnLegion : MonoBehaviour
{
    [SerializeField]
    private Transform[] legions;
    [SerializeField]
    private Transform[] monsters;
    [SerializeField]
    private Transform[] heroPos;
    public GameObject[] Legion;
    public List<AIHeroes> Heroes;
    public GameObject Dragon;
    public GameObject Red;
    public GameObject Blue;
    public GameObject MinionMonster;
    public GameObject RightMonster;
    public GameObject LeftMonster;

    private Stopwatch timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        //InvokeRepeating("SpawnBaseOnCondition", 1, 1);
        Invoke("SpawnBaseOnCondition", 1);
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
        foreach (var item in heroPos)
        {
            if (item.name.StartsWith("HeroEnemy"))
            {
                int number = 0;
                foreach (Transform posChild in item)
                {
                    if (number < 1)
                    {
                        var obj = Instantiate(Heroes[number], posChild.transform.position, Quaternion.identity, posChild.transform);
                      
                        obj.GetComponent<Character>().Team = TeamCharacter.Red;
                        obj.tag = "Enemy";
                        number++;
                    }

                }
            }
            //else if (item.name.StartsWith("HeroAlly"))
            //{
            //    int number = 0;
            //    foreach (Transform posChild in item)
            //    {
            //        if (number < 3)
            //        {
            //            var obj = Instantiate(Heroes[number], posChild.transform.position, Quaternion.identity, posChild.transform);
            //            obj.tag = "Ally";
            //            number++;
            //        }

            //    }
            //}

        }

        //if (timer.Elapsed.TotalSeconds % 2 == 0)
        //{
        //foreach (var legion in legions)
        //{
        //    if (legion.transform.name.StartsWith("LegionEnemy"))
        //    {
        //        foreach (var item in Legion)
        //        {
        //            var obj = Instantiate(item, legion.transform.position, Quaternion.identity, legion.transform);
        //            obj.tag = "Enemy";
        //        }
        //    }
        //    else if(legion.transform.name.StartsWith("LegionAlly"))
        //    {
        //        foreach (var item in Legion)
        //        {
        //            var obj = Instantiate(item, legion.transform.position, Quaternion.identity, legion.transform);

        //            obj.tag = "Ally";
        //        }
        //    }
        //}
        //}
        //else if ((int)timer.Elapsed.TotalSeconds % 4 == 0)
        //{
        //    foreach (var monster in monsters)
        //    {

        //        if (monster.name == "Dragon")
        //        {

        //            Instantiate(Dragon, monster.transform.position, Quaternion.identity);
        //        }
        //        else if (monster.name == "Blue")
        //        {
        //            Instantiate(Blue, monster.transform.position, Quaternion.identity);

        //        }
        //        else if (monster.name == "Red")
        //        {
        //            Instantiate(Red, monster.transform.position, Quaternion.identity);

        //        }
        //        else if(monster.name.StartsWith("MinionRight"))
        //        {
        //            Instantiate(RightMonster, monster.transform.position, Quaternion.identity);

        //        }
        //        else if (monster.name.StartsWith("MinionLeft"))
        //        {
        //            Instantiate(LeftMonster, monster.transform.position, Quaternion.identity);

        //        }
        //        else
        //        {
        //            Instantiate(MinionMonster, monster.transform.position, Quaternion.identity);

        //        }
        //    }

        //}
    }
}

