using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using DG.Tweening;
using CharacterMechanism.System;

[DefaultExecutionOrder(1500)]
[RequireComponent(typeof(RectTransform))]
public class AbilitySkillButton : MonoBehaviour
{
    public static AbilitySkillButton Create(string name, Vector3 position, AbilitySkillUsePanel abilitySkillUsePanel)
    {
        AbilitySkillButton abilitySkillButton = new GameObject("Skill Alpha Button", typeof(AbilitySkillButton)).GetComponent<AbilitySkillButton>();
        abilitySkillButton.transform.SetParent(abilitySkillUsePanel.transform);

        RectTransform skillUIChild = new GameObject("Skill UI", typeof(RectTransform)).GetComponent<RectTransform>();
        skillUIChild.transform.SetParent(abilitySkillButton.transform);
        skillUIChild.localPosition = Vector3.zero;
        skillUIChild.sizeDelta = new Vector2(100f, 100f);

        Image skillImage = new GameObject("Skill Image", typeof(Image)).GetComponent<Image>();
        //skillImage.gameObject.

        return abilitySkillButton;
    }
}

[DefaultExecutionOrder(1500)]
[RequireComponent(typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster))]
public class AbilitySkillUsePanel : MonoBehaviour
{
    //enum ButtonAbility
    //{
    //    AlphaSkill,
    //    BetaSkill,
    //    UltimateSkill,
    //}


    //readonly Dictionary<ButtonAbility, Vector3> buttonPositions = new Dictionary<ButtonAbility, Vector3>();
    //Hashtable hashtable = new Hashtable();

    //void Awake()
    //{
    //    buttonPositions.Add(ButtonAbility.AlphaSkill, Vector3.zero);
    //}

    static readonly Vector3 PositionAlphaSkillButton = new Vector3(196.6f, -170.8f);
    static readonly Vector3 PositionBetaSkillButton = new Vector3(245f, -87.9f);
    static readonly Vector3 PositionUltimateSkillButton = new Vector3(326.5f, -38.9f);

    static readonly string NameAlphaSkillButton = "Skill Alpha Button";
    static readonly string NameBetaSkillButton = "Skill Beta Button";
    static readonly string NameUltimateSkillButton = "Skill Ultimate Button";

    AbilitySkillButton alphaSkillButton;
    AbilitySkillButton betaSkillButton;
    AbilitySkillButton ultimateSkillButton;

    CharacterSystem characterSystem;

    static public AbilitySkillUsePanel Create(CharacterSystem characterSystem)
    {
        AbilitySkillUsePanel abilitySkillUsePanel = new GameObject("Ability Skill Use Panel", typeof(AbilitySkillUsePanel)).GetComponent<AbilitySkillUsePanel>();
        abilitySkillUsePanel.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        abilitySkillUsePanel.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        abilitySkillUsePanel.alphaSkillButton = AbilitySkillButton.Create(NameAlphaSkillButton, PositionAlphaSkillButton, abilitySkillUsePanel);
        abilitySkillUsePanel.betaSkillButton = AbilitySkillButton.Create(NameBetaSkillButton, PositionUltimateSkillButton, abilitySkillUsePanel);
        abilitySkillUsePanel.ultimateSkillButton = AbilitySkillButton.Create(NameUltimateSkillButton, PositionUltimateSkillButton, abilitySkillUsePanel);

        return abilitySkillUsePanel;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
