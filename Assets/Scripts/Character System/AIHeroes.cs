using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;


[DefaultExecutionOrder(1500)]
public class AIHeroes : MonoBehaviour
{
    [SerializeField]
    private float radiusCollider = 5;
    AIPath aiPath;
    AIDestinationSetter aiSetter;
    private bool isTargetHere = false;
    private Transform objectTarget;
    private Transform objectTargetCharacter;
    private bool isTowerHere;
    private bool isRunning;
    private bool isChasing;
    private List<Transform> listTower = new List<Transform>();
    private Animator anim;
    private bool isPlayer;
    private Character character;
    List<Collider> listCollider = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();

        //test thu khi chon 1 tuong thi se ko gan AI vao
        if (GetComponent<PlayerController>())
        {
            isPlayer = true;
            return;
        }
        //set gia tri AI dung' tren map
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        aiSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        anim = GetComponent<Animator>();

        //UnityEngine.Debug.Log(string.Format("AIHero: {0}", character));

        aiPath.endReachedDistance = character.Stats.RangeAttack.Value;
        //UnityEngine.Debug.Log(aiPath.endReachedDistance);
        print(aiPath.endReachedDistance);


        //khoi tao chien thuat tu 2 phe khac nhau 
        StrategyMainTarget();
    }

    private void StrategyMainTarget() // cau truc lai
    {
        if (transform.parent.CompareTag("PosEnemy")) // cac string can cau truc lai qua scriptable 
        {
            var objTower = GameObject.Find("TowerRed");
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
            var objTower = GameObject.Find("TowerBlue");
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
        //neu hero da duoc chon se ko chay AI
        if (isPlayer) return;
        //check dieu kien tower ket thuc thi se ko chay nua 
        if (listTower.Count > 0)
            CheckAreaToAttack(transform.position, radiusCollider);
        else
        {
            anim.SetBool("isMove", false);
            anim.SetBool("isAttack", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusCollider);
    }
    void CheckAreaToAttack(Vector3 center, float radius)
    {
        listCollider.Clear();

        isTowerHere = false;
        isTargetHere = false;

        GetListCollider(center, radius);

        ActionWithColliderBaseOnCondition();

        if (isTargetHere && isRunning == false)
        {
            isRunning = true;
            StartCoroutine(ChasingPlayerInTime());
        }

        //kiem tra ke dich xa hay gan cho animation
        AnimationBaseOnConditional();

        aiSetter.target = objectTarget;
    }

    private void AnimationBaseOnConditional()
    {
        if (aiPath.reachedEndOfPath == false)
        {
            character.Attack();

            anim.SetBool("isAttack", false);
            anim.SetBool("isMove", true);
        }
        else
        {
            //character.Attack();
            anim.SetBool("isAttack", true);
            anim.SetBool("isMove", false);
        }
    }

    private void ActionWithColliderBaseOnCondition()
    {
        foreach (var hitCollider in listCollider)
        {
            Character characterHitCollider = hitCollider.GetComponent<Character>();
            //print(characterHitCollider);

            if (Character.IsTarget(characterHitCollider, character))
            {
                if (hitCollider.transform.name.StartsWith("TowerChild"))//TypeCharacter
                {
                    isTowerHere = true;
                }
                else
                {
                    isTargetHere = true;
                    objectTargetCharacter = hitCollider.transform;
                    if (isChasing == false)
                        objectTarget = hitCollider.transform;
                }


                if (isTowerHere && isTargetHere && isChasing == false)
                {
                    isTargetHere = true;
                    objectTarget = objectTargetCharacter;
                }
            }


            //kiem tra va cham neu khong phai la dong minh 
            //if (hitCollider.tag != transform.tag) //TeamCharacter
            //{

            //}
        }

        if (isTargetHere == false && isTowerHere == false)
        {
            objectTarget = listTower[0];
        }
    }
    // loc ra cac object va cham chinh' trong battle
    private void GetListCollider(Vector3 center, float radius) // cau truc lai 
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<Character>())//collider.CompareTag("Tower") || collider.CompareTag("Ally") || collider.CompareTag("Enemy") || collider.CompareTag("Creep"))
            {
                listCollider.Add(collider);
            }
        }
    }
    IEnumerator ChasingPlayerInTime()
    {
        isChasing = true;
        Stopwatch timer = new Stopwatch();
        int countDown = 3;
        timer.Start();

        while (timer.Elapsed.TotalSeconds < countDown && isTargetHere)
        {
            yield return null;
            if (aiPath.reachedEndOfPath)
            {
                timer.Restart();
                aiSetter.target = objectTarget;
                anim.SetBool("isAttack", true);
                anim.SetBool("isMove", false);
            }
            else
            {
                anim.SetBool("isAttack", false);
                anim.SetBool("isMove", true);
            }
        }
        timer.Stop();
        objectTarget = listTower[0];
        yield return new WaitForSeconds(3);
        isChasing = false;
        isRunning = false;

    }



}
