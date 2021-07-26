using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseInformation
{
    static public string GenerateID()
    {
        return Guid.NewGuid().ToString();
    }

    static public string GenerateResourcePath(ResourceType resourceType, Character character)
    {
        switch (resourceType)
        {
            case ResourceType.Icon:
                return string.Format("Images/Icons/{0}/{1}", character.TypeCharacter, character.Name);
            default:
                break;
        }


        return string.Format("");

    }
}
