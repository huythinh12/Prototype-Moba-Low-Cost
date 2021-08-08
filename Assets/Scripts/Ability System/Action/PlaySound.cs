using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

public class PlaySound : BaseAction
{
    private float delay;
    private List<StringField> soundPaths;

    public PlaySound(AbilityActionData data) : base(data)
    {
        soundPaths = this.data.StringFields;
        delay = this.data.FloatFields.Find((a) => a.Name == "delay").Value;
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, CharacterSystem selfCharacter, CharacterSystem targetCharacter)
    {
        yield return new WaitForSeconds(delay);

        string soundPathRandom = soundPaths[Random.Range(0, soundPaths.Count)].Value;
        selfCharacter.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(soundPathRandom));

        yield return null;
    }

    public override BaseAction Clone()
    {
        return new PlaySound(data);
    }
}
