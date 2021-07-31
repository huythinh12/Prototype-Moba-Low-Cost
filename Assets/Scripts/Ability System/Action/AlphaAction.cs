using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaAction : BaseAction
{
    public AlphaAction(AbilityActionData data) : base(data)
    {
    }

    public override BaseAction Clone()
    {
        return new AlphaAction(data);
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, Character selfCharacter, Character targetCharacter)
    {
        Debug.Log("AlphaAction");

        yield return null;
    }
}
