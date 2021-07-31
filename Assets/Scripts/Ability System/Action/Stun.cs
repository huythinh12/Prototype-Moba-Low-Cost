//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Stun : BaseAction
//{
//    float duration;

//    public Stun(AbilityActionData data):base(data)
//    {
//        duration = data.FloatFields.Find((a) => a.Name == "Duration").Value;
//    }
    
//    public override IEnumerator Excecute(Ability owner, GameObject target, Vector3 targetPoint)
//    {
//        var targetCharacter = target.GetComponent<Character>();

//        if (targetCharacter)
//        {
//            var effectObjject = GameObject.Instantiate(Resources.Load<GameObject>(effectPath), targetCharacter.AttachPoint.Overhead.position, Quaternion.identity);
//            effectObjject.transform.SetParent(targetCharacter.AttachPoint.Overhead);
//            effectObjects.Add(effectObjject);

//            //targetCharacter.navAgent.isStopped = true;
//            //targetCharacter.navAgent.ResetPath();
//            targetCharacter.CombatController.IsStuned = true;

//            yield return new WaitForSeconds(duration);

//            targetCharacter.CombatController.IsStuned = false;
//        }

//        throw new System.NotImplementedException();
//    }

//    public override BaseAction Clone()
//    {
//        throw new System.NotImplementedException();
//    }
//}
