using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSound
{
    public enum TypeSound
    {
        Movement,
        Die,
    }


    [SerializeField] public List<string> soundPathsMovement = new List<string>();
    [SerializeField] List<string> soundPathsDie = new List<string>();


    public AudioClip GetRandomSound(CharacterSound.TypeSound typeSound)
    {
        List<string> soundPaths = GetListSoundPaths(typeSound);
        string soundPathRandom = soundPaths[Random.Range(0, soundPaths.Count)];

        return Resources.Load<AudioClip>(soundPathRandom);
    }
    private List<string> GetListSoundPaths(CharacterSound.TypeSound typeSound)
    {
        switch (typeSound)
        {
            case TypeSound.Movement:
                return soundPathsMovement;
            case TypeSound.Die:
                return soundPathsDie;
        }

        return null;
    }
}
