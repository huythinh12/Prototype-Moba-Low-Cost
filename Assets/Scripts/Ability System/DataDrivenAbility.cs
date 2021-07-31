using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;

[Flags]
public enum BehaviorFlag
{
    Hidden = 1 << 0, //Can be owned by a unit but can't be cast and won't show up on the HUD.
    Passive = 1 << 1, //Cannot be cast like above but this one shows up on the ability HUD.
    NoTarget = 1 << 2, //Doesn't need a target to be cast, ability fires off as soon as the button is pressed.
    UnitTarget = 1 << 3, //Needs a target to be cast on.
    PointTarget = 1 << 4, //Can be cast anywhere the mouse cursor is (if a unit is clicked it will just be cast where the unit was standing).
    AOE = 1 << 5, //Draws a radius where the ability will have effect. Kinda like POINT but with a an area of effect display.
    Channelled = 1 << 6, //Channeled ability. If the user moves or is silenced the ability is interrupted.
    Item = 1 << 7, //Ability is tied up to an item.
    Directional = 1 << 8, //Has a direction from the hero, such as miranas arrow or pudge's hook.
    Immediate = 1 << 9, //Can be used instantly without going into the action queue.
    AutoCast = 1 << 10, //Can be cast automatically.
    Aura = 1 << 11, //Ability is an aura.  Not really used other than to tag the ability as such.
    RootDisables = 1 << 12, //Cannot be used when rooted
    DontCancleMovement = 1 << 13, //Doesn't cause certain modifiers to end, used for courier and speed burst.
}

[Flags]
public enum TargetFlag
{
    Self = 1 << 0,
    Enemy = 1 << 1,
    Ground = 1 << 2,
}

public enum Target
{
    Caster,
    Target,
    Point,
}

public class DataDrivenAbility : ScriptableObject
{
    public AbilityData abilityData = new AbilityData();

    [SerializeField]
    public List<AbilityEventData> events = new List<AbilityEventData>();

    public DataDrivenAbility()
    {
    }

    public static Ability Parse(DataDrivenAbility rawAbility)
    {
        Ability ability = new Ability(rawAbility.abilityData);

        foreach (var abilityEvent in rawAbility.events)
        {
            foreach (var action in abilityEvent.Actions)
            {
                // get the type of action class
                var actionType = System.Type.GetType(action.Type.ToString());
                var actionObj = (BaseAction)System.Activator.CreateInstance(actionType, action);

                // register the corresponding event callback in ability class
                ability.EventRegister[abilityEvent.Type].Add(actionObj);
            }
        }

        return ability;
    }
}