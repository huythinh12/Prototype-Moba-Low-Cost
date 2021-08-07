//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum FindTargetMode
//{
//    LowestHP,
//    Nearest,
//}

//public enum TeamTarget
//{
//    Ally,
//    Enemy,
//}

//public class AbilityBehaviors : MonoBehaviour
//{
//    public static Character FindTarget(Character characterSelf, float radius, List<TypeCharacter> typesCharacter, TeamTarget teamTarget, FindTargetMode mode)
//    {
//        List<Character> characterTargets = GetCharactersByOverlapSphere(characterSelf.transform.position, radius);
//        characterTargets.Remove(characterSelf);
//        Character finalCharacterTarget = new Character();

//        switch (teamTarget)
//        {
//            case TeamTarget.Ally:
//                {
//                    for (int i = 0; i < characterTargets.Count; i++)
//                    {
//                        if (characterTargets[i].Team == characterSelf.Team) { Debug.Log("Hello!"); }
//                        else
//                        {
//                            Debug.Log("Xoa r!");
//                            characterTargets.Remove(characterTargets[i]);
//                        }
//                    }

//                    break;
//                }

//            case TeamTarget.Enemy:
//                {
//                    for (int i = 0; i < characterTargets.Count; i++)
//                    {
//                        if (characterTargets[i].Team == characterSelf.Team)
//                        {
//                            characterTargets.Remove(characterTargets[i]);
//                        }
//                    }

//                    break;
//                }
//        }

//        if (characterTargets.Count == 0)
//        {
//            return null;
//        }

//        switch (mode)
//        {
//            case FindTargetMode.LowestHP:
//                {
//                    finalCharacterTarget = characterTargets[0];

//                    foreach (var characterTarget in characterTargets)
//                    {
//                        if (finalCharacterTarget.Stats.HealthCurrent > characterTarget.Stats.HealthCurrent)
//                        {
//                            finalCharacterTarget = characterTarget;
//                        }
//                    }

//                    break;

//                }

//            case FindTargetMode.Nearest:
//                {
//                    finalCharacterTarget = characterTargets[0];

//                    foreach (var characterTarget in characterTargets)
//                    {
//                        if (Vector3.Distance(finalCharacterTarget.transform.position, characterSelf.transform.position) > Vector3.Distance(characterTarget.transform.position, characterSelf.transform.position))
//                        {
//                            finalCharacterTarget = characterTarget;
//                        }
//                    }

//                    break;
//                }
//        }


//        Debug.Log(finalCharacterTarget);
//        return finalCharacterTarget;
//    }

//    public static List<Character> GetCharactersByOverlapSphere(Vector3 position, float radius)
//    {
//        Collider[] colliders = Physics.OverlapSphere(position, radius);
//        List<Character> characters = new List<Character>();

//        foreach (var collider in colliders)
//        {
//            if (collider.GetComponent<Character>() == null)
//            {

//            }
//            else
//            {
//                characters.Add(collider.GetComponent<Character>());
//            }
//        }

//        return characters;
//    }


//    public static bool IsTargetBasedOnTypeCharacter(Character characters, List<TypeCharacter> types)
//    {
//        foreach (var type in types)
//        {
//            if (characters.TypeCharacter == type)
//            {
//                return true;
//            }
//        }

//        return false;
//    }

//    public static bool IsTargetBasedOnTeamCharacter(Character character, List<TeamCharacter> teams)
//    {

//    }
//}
