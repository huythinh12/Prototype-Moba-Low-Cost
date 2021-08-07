using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CharacterMechanism.System;

public enum Action
{
    PlaySound,
    PlayEffect,
    AttachEffect,
    ImpactWithStat,
    ImpactWithCamera,
    Roll,

    //[Description("Takes maxDistance:Int, effectPath:Str")]
    //Blink,
    //[Description("Takes force:Int")]
    //Knockback,
    //[Description("Takes effectPath:Str, speed:Int, origin:Str(cast_point||target_point)")]
    //LinearProjectile,
    //[Description("Takes effectPath:Str, speed:Int")]
    //TrackingProjectile,
    //[Description("Takes duration:Float")]
    //Stun,
}

public abstract class BaseAction
{
    public AbilityActionData data;
    protected List<GameObject> effectObjects = new List<GameObject>();
    protected CharacterSystem targetCharacter;

    public BaseAction(AbilityActionData data)
    {
        this.data = data;
    }

    public abstract IEnumerator Excecute(Ability owner, Vector3 indicator, CharacterSystem selfCharacter, CharacterSystem targetCharacter);

    public virtual IEnumerator Reset()
    {
        yield return null;
    }

    //protected GameObject CreateEffect(Vector3 position)
    //{
    //    var obj = PhotonNetwork.Instantiate(effectPath, position, Quaternion.identity, 0) as GameObject;
    //    obj.AddComponent<SelfDestoryParticle>();
    //    return obj;
    //}

    public abstract BaseAction Clone();

}
