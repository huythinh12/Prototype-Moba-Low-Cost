//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Damage : BaseAction
//{
//    private int amount;

//    public Damage(AbilityActionData data) : base(data)
//    {
//        amount = this.data.IntFields.Find((a) => a.Name == "Amount").Value;
//    }

//    public override IEnumerator Excecute(Ability owner, Vector3 indicator, Character self, GameObject target)
//    {
//        Character characterTarget = target.GetComponent<Character>();

//        if (targetCharacter)
//        {
//            characterTarget.CombatController.TakeDamage(self, DamageType.Physical, 850f);
//        }

//        yield return null;
//    }

//    public override BaseAction Clone()
//    {
//        return new Damage(this.data);
//    }
//}
