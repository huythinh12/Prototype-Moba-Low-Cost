//using UnityEngine;

//[System.Serializable]
//public class AbilityData
//{
//    public bool CanTargetSelf
//    {
//        get
//        {
//            return ((TargetFlags & TargetFlag.Self) != 0);
//        }
//    }
//    public bool CanTargetEnemy
//    {
//        get
//        {
//            return ((TargetFlags & TargetFlag.Enemy) != 0);
//        }
//    }
//    public bool CanTargetGround
//    {
//        get
//        {
//            return ((TargetFlags & TargetFlag.Ground) != 0);
//        }
//    }

//    public BehaviorFlag BehaviorFlags;
//    public TargetFlag TargetFlags;

//    public string Name;

//    [TextArea(5, 10)]
//    public string Description;
//    public float Cooldown;
//    public float ManaCost;
//    public string Animation = "Spell1";
//    public float AnimationCastPoint;
//    public Sprite Icon;
//    public bool canCritical;

//    public AoeData AoeData;
//    public float Duration;

//    [Space(), Space()]
//    [Header("Cast Skill - Indicator")]
//    public IndicatorAbilityType indicatorAbilityType;

//    public AbilityStat RangeCast;

//    [Header("For Indicator Circle")]
//    public AbilityStat Radius;

//    [Header("For Indicator Rectangle")]
//    public AbilityStat WidthAreaOfEffect;

//    //[Space()]
//    //public AbilityStat 

//}