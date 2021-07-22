using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class AILegion : MonoBehaviour
{
    public bool isEnemy;
    AIPath aiPath;
    LayerMask zone;
    AIDestinationSetter aiSetter;
    private Transform objectTarget;
    private List<Transform> listTower = new List<Transform>();
    private Animator anim;
    private bool isTowerHere;
    private bool isRunning;
    private bool isTargetHere;
    private bool isLegion = true;

    // Start is called before the first frame update
    void Start()
    {

        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        aiSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        zone = LayerMask.GetMask("Zone");
        anim = GetComponent<Animator>();

        // check to find tower enemy 
        if (transform.parent.CompareTag("PosEnemy"))
        {
            isEnemy = true;
            var objTower = GameObject.Find("TowerA");
            if (objTower != null)
                foreach (Transform child in objTower.transform)
                {
                    listTower.Add(child);
                }
        }
        else
        {
            isEnemy = false;
            var objTower = GameObject.Find("TowerB");
            if (objTower != null)
                foreach (Transform child in objTower.transform)
                {
                    listTower.Add(child);
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (listTower.Count > 0)
            CheckAreaToAttack(transform.position, 3);
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttack", false);
        }
   
    }

    private void CheckAreaToAttack(Vector3 center, float radius)
    {
        isTowerHere = false;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, ~zone);
        foreach (var hitCollider in hitColliders)
        {

            //check if collide with tower 
            if (hitCollider.CompareTag("Tower"))
            {
                isTowerHere = true;
                if (isEnemy == false && hitCollider.transform.parent.CompareTag("TowerB"))
                {
                    //test bam nut D de destroy tower va huong toi tower tiep theo
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        Destroy(hitCollider.gameObject);
                        listTower.Remove(listTower[0]);
                        aiPath.isStopped = false;
                        isTowerHere = false;
                        return;
                    }
                    aiPath.isStopped = true;
                    aiSetter.target = transform;
                    anim.SetBool("isAttack", true);
                    anim.SetBool("isMoving", false);

                }
                else if (isEnemy == true && hitCollider.transform.parent.CompareTag("TowerA"))
                {
                    //test bam nut D de destroy tower va huong toi tower tiep theo
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        Destroy(hitCollider.gameObject);
                        listTower.Remove(listTower[0]);
                        aiPath.isStopped = false;
                        isTowerHere = false;
                        return;
                    }
                    aiPath.isStopped = true;
                    aiSetter.target = transform;
                    anim.SetBool("isAttack", true);
                    anim.SetBool("isMoving", false);

                }
            }
            else if (hitCollider.tag != transform.tag)
            {
                isTargetHere = true;
                objectTarget = hitCollider.transform;
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
        int countDown = 0;
       
        timer.Start();
        while ((int)timer.Elapsed.TotalSeconds < countDown && isTargetHere && objectTarget !=null)
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
