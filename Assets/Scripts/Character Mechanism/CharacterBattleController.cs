using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

public class CharacterBattleController : MonoBehaviour
{
    private CharacterSystem characterSystem;

    public Ability attackAbility;
    public Ability alphaAbility;
    public Ability betaAbility;
    public Ability ultimateAbility;

    // Start is called before the first frame update
    void Start()
    {
        this.characterSystem = gameObject.GetComponent<CharacterSystem>();

        attackAbility = characterSystem.GetProfile.AttackAbilityName != null ? AbilityDatabase.Instance.GetAbility(characterSystem.GetProfile.AttackAbilityName) : null;
        alphaAbility = characterSystem.GetProfile.AlphaAbilityName != null ? AbilityDatabase.Instance.GetAbility(characterSystem.GetProfile.AlphaAbilityName) : null;
        betaAbility = characterSystem.GetProfile.BetaAbilityName != null ? AbilityDatabase.Instance.GetAbility(characterSystem.GetProfile.BetaAbilityName) : null;
        ultimateAbility = characterSystem.GetProfile.UltimateAbilityName != null ? AbilityDatabase.Instance.GetAbility(characterSystem.GetProfile.UltimateAbilityName) : null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
