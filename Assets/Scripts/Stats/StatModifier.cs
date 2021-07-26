using System;
using System.Collections.Generic;

public enum StatModifyType
{
    Flat = 10,
    PercentAdd = 20,
    PercentMulti = 30,
}

public class StatModifier
{
    public readonly float Value;
    public readonly StatModifyType Type;
    public readonly int Order;
    public readonly object Source;

    public StatModifier(float value, StatModifyType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }

    public StatModifier(float value, StatModifyType type) : this(value, type, (int)type) { }

    public StatModifier()
    {

    }

}
