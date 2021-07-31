using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class BaseAction
{
    public AbilityActionData data;
    protected string effectPath;
    protected string sfxPath;
    protected List<GameObject> effectObjects = new List<GameObject>();
    protected Character targetCharacter;

    public BaseAction(AbilityActionData data)
    {
        this.data = data;
    }

    public abstract IEnumerator Excecute(Ability owner, Vector3 indicator, Character selfCharacter, Character targetCharacter);

    //public virtual IEnumerator Reset()
    //{
    //    for (int i = 0; i < effectObjs.Count; i++)
    //    {
    //        PhotonNetwork.Destroy(effectObjs[i]);
    //    }
    //    effectObjs.Clear();
    //    yield return null;
    //}

    //protected GameObject CreateEffect(Vector3 position)
    //{
    //    var obj = PhotonNetwork.Instantiate(effectPath, position, Quaternion.identity, 0) as GameObject;
    //    obj.AddComponent<SelfDestoryParticle>();
    //    return obj;
    //}

    public abstract BaseAction Clone();

}
