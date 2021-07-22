using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;
public class AIHeroes : MonoBehaviour
{
    AIPath aiPath;
    public bool isEnemy;
    LayerMask zone;
    AIDestinationSetter aiSetter;
    private bool isTargetHere;
    private Transform objectTarget;
    private bool isTowerHere;
    private bool isRunning;
    private List<Transform> listTower = new List<Transform>();
    private Animator anim;
    private bool isPlayer;
    List<Collider> listCollider = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        if (transform.name.StartsWith("Noah"))
        {
            isPlayer = true;
            return;
        }
        aiSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        zone = LayerMask.GetMask("Zone");
        anim = GetComponent<Animator>();


        if (transform.parent.CompareTag("PosEnemy"))
        {
            isEnemy = true;
            var objTower = GameObject.Find("TowerA");
            if (objTower != null)
            {
                foreach (Transform child in objTower.transform)
                {
                    listTower.Add(child);
                }

            }
        }
        else
        {
            isEnemy = false;
            var objTower = GameObject.Find("TowerB");
            if (objTower != null)
            {
                foreach (Transform child in objTower.transform)
                {
                    listTower.Add(child);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer) return;
        if (listTower.Count > 0)
            CheckAreaToAttack(transform.position, 2.5f);
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttack", false);
        }
    }
    void CheckAreaToAttack(Vector3 center, float radius)
    {
        isTowerHere = false;
        listCollider.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, ~zone);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Untagged") == false)
            {
                listCollider.Add(collider);
            }
        }

        foreach (var hitCollider in listCollider)
        {
            //check if collide with tower 
            if (hitCollider.CompareTag("Tower"))
            {
                print("co va cham tower");
                isTowerHere = true;
                isTargetHere = false;
                if (isEnemy == false && hitCollider.transform.parent.CompareTag("TowerB"))
                {
                    aiPath.isStopped = true;
                    anim.SetBool("isAttack", true);
                    anim.SetBool("isMoving", false);
                    //test bam nut D de destroy tower va huong toi tower tiep theo
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        Destroy(hitCollider.gameObject);
                        aiPath.isStopped = false;
                        listTower.Remove(listTower[0]);
                        isTowerHere = false;
                        return;
                    }
                   

                }
                else if (isEnemy == true && hitCollider.transform.parent.CompareTag("TowerA"))
                {
                        aiPath.isStopped = true;
                        anim.SetBool("isAttack", true);
                        anim.SetBool("isMoving", false);
                      
                    //test bam nut D de destroy tower va huong toi tower tiep theo
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        Destroy(hitCollider.gameObject);
                        aiPath.isStopped = false;

                        listTower.Remove(listTower[0]);
                        isTowerHere = false;
                        return;
                    }

                }
            }
            //else if (hitCollider.CompareTag("HeroEnemy") && isEnemy == false)
            //{
            //    isTargetHere = true;
            //    objectTarget = hitCollider.transform;
            //    aiSetter.target = objectTarget;
            //    anim.SetBool("isAttack", true);
            //    anim.SetBool("isMoving", false);
            //}
            //else if (hitCollider.CompareTag("HeroAlly") && isEnemy == true)
            //{
            //    isTargetHere = true;
            //    objectTarget = hitCollider.transform;
            //    aiSetter.target = objectTarget;
            //    anim.SetBool("isAttack", true);
            //    anim.SetBool("isMoving", false);
            //}
            else if (hitCollider.tag != transform.tag && isTowerHere == false)
            {
              
                isTargetHere = true;
                objectTarget = hitCollider.transform;
                transform.LookAt(objectTarget);
                aiSetter.target = objectTarget;
                anim.SetBool("isAttack", true);
                anim.SetBool("isMoving", false);
            }
        }
        if (isTargetHere && isRunning == false)
        {
            isRunning = true;
            StartCoroutine(ChasingPlayerInTime());
        }
        else if (isTowerHere == false && isTargetHere == false)
        {
            objectTarget = listTower[0];
            aiSetter.target = objectTarget;
            anim.SetBool("isMoving", true);
            anim.SetBool("isAttack", false);
            isRunning = false;
        }
    }
    IEnumerator ChasingPlayerInTime()
    {
        Stopwatch timer = new Stopwatch();
        int countDown = 8;
        timer.Start();
        while ((int)timer.Elapsed.TotalSeconds < countDown && isTargetHere && objectTarget != null && isTowerHere == false)
        {
            yield return null;
            if (aiPath.reachedEndOfPath)
            {
                timer.Restart();
                isTargetHere = true;
                anim.SetBool("isAttack", true);
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isAttack", false);
                anim.SetBool("isMoving", true);
            }
        }

        timer.Stop();
        isTargetHere = false;
    }

}
