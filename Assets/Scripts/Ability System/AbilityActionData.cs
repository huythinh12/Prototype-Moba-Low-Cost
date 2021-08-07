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
