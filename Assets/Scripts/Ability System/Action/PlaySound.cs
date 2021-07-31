using UnityEngine;
using System.Collections;

public class PlaySound : BaseAction
{
    private string pathSound;

    public PlaySound(AbilityActionData data) : base(data)
    {
        pathSound = this.data.StringFields.Find((a) => a.Name == "PathSound").Value;
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, Character selfCharacter, Character targetCharacter)
    {
        selfCharacter.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(pathSound));
        yield return null;
    }

    public override BaseAction Clone()
    {
        return new PlaySound(data);
    }
}
