using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

public class TestIndicatir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterSystem character = new CharacterSystem();

        AbilityIndicatorUI.Create(character);
        AbilitySkillUsePanel.Create(character);

    }
}
