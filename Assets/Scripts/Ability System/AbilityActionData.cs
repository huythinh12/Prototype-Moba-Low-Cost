using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

public enum TypeStat
{
    RangeAttack,
    MovementSpeed,
    PhysicalDamage,
    MagicDamage,
}

public enum Action
{
    [Description("Takes PathSound:Str")]
    PlaySound,
    [Description("Takes effectPath:Str")]
    PlayEffect,
    [Description("Takes effectPath:Str")]
    AttachEffect,

    ImpactWithStat,
    ImpactWithCamera,

    [Description("Takes maxDistance:Int, effectPath:Str")]
    Blink,
    [Description("Takes force:Int")]
    Knockback,
    [Description("Takes effectPath:Str, speed:Int, origin:Str(cast_point||target_point)")]
    LinearProjectile,
    [Description("Takes effectPath:Str, speed:Int")]
    TrackingProjectile,
    [Description("Takes duration:Float")]
    Stun,
    Move,

}

[System.Serializable]
public class AbilityActionData
{
    public Action Type;
    public Target Target;
    public bool MultipleTargets;

    // custom properties
    public TypeStat TypeStats = new TypeStat();
    public List<IntField> IntFields = new List<IntField>();
    public List<FloatField> FloatFields = new List<FloatField>();
    public List<Vector3Field> Vector3Fields = new List<Vector3Field>();
    public List<Vector2Field> Vector2Fields = new List<Vector2Field>();
    public List<StringField> StringFields = new List<StringField>();
    public List<BoolField> BoolFields = new List<BoolField>();
    // editor flags
    public bool showInEditor = true;
    public bool tobeRemoved;
    public bool showCustomAttributes;
}
