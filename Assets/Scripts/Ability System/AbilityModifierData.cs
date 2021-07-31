using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityModifierData
{
    public enum Modifier
    {
        MaxHP,
        Speed,
        CooldownMultiplier,
        KnockbackMultiplier,
    }
    public bool showInEditor = true;
    public bool tobeRemoved;
    public Modifier Type;
    public Target Target;
    public List<IntField> IntFields = new List<IntField>();
    public List<FloatField> FloatFields = new List<FloatField>();
    public List<Vector3Field> Vec3Fields = new List<Vector3Field>();
    public List<Vector2> Vec2Fields = new List<Vector2>();
    public List<StringField> StrFields = new List<StringField>();
    public List<BoolField> BoolFields = new List<BoolField>();
}