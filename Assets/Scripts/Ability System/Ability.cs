using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.System;

[System.Serializable]
public class Ability
{
    protected ButtonPosition buttonPosition;
    public ClassifyAbility classifyAbility;

    public bool PointTarget
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.POINT_TARGET) != 0);
        }
    }
    public bool UnitTarget
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.UNIT_TARGET) != 0);
        }
    }
    public bool NoTarget
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.NO_TARGET) != 0);
        }
    }
    public bool AOE
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.AOE) != 0);
        }
    }
    public bool Channelled
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.CHANNELLED) != 0);
        }
    }
    public bool Hidden
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.HIDDEN) != 0);
        }
    }

    public bool Immediate
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.IMMEDIATE) != 0);
        }
    }
    public bool DontCancelMovement
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.DONT_CANCEL_MOVEMENT) != 0);
        }
    }

    public bool Directional
    {
        get
        {
            return true;//((abilityData.BehaviorFlags & DataDrivenAbility.BehaviorFlag.DIRECTIONAL) != 0);
        }
    }

    public float CurrentCooldown { get; set; }
    public bool IsOnCooldown
    {
        get
        {
            return CurrentCooldown > 0;
        }
    }


    public AbilityData abilityData = new AbilityData();
    public Dictionary<EventAbility, List<BaseAction>> EventRegister = new Dictionary<EventAbility, List<BaseAction>>();

    //Internal Vars
    public CharacterSystem selfCharacter { get; private set; }
    public List<CharacterSystem> targetCharacters { get; private set; }
    public Vector3 indicator { get; private set; }

    public bool isRunning { get; private set; }
    public bool isCast { get; private set; }
    public Vector3 targetPoint { get; private set; } // cast to target post
    public GameObject targetUnit { get; private set; }
    public Vector3 castPoint { get; private set; } // owner's position when cast
    public List<GameObject> targets { get; private set; }


    public Ability(AbilityData data)
    {
        abilityData = data;
        targetCharacters = new List<CharacterSystem>();

        EventRegister.Add(EventAbility.OnAbilityStart, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnSpellStart, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnChannelInterrupted, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnChannelSucceed, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnProjectileHitUnit, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnTargetDied, new List<BaseAction>());
        EventRegister.Add(EventAbility.OnUpgrade, new List<BaseAction>());
    }

    public IEnumerator Excecute(CharacterSystem casterCharacter, Vector3 indicator)
    {
        this.selfCharacter = casterCharacter;


        if (selfCharacter.IsAlive)
        {
            this.indicator = indicator;

            isRunning = true;

            // Extract Target Point / Unit
            Vector3 castPoint = casterCharacter.transform.position;
            // Vector3 targetPoint = targetCharacter[0].transform.position;
            // GameObject targetUnit = (GameObject)targetCharacter[1];

            if (EventRegister[EventAbility.OnAbilityStart] != null)
            {
                Debug.Log("On Ability Start!");
                yield return PerformActions(EventRegister[EventAbility.OnAbilityStart]);
            }

            // Turn first
            if (!DontCancelMovement)
            {
                // restrict movement
                //casterCharacter.navAgent.isStopped = true;
                //casterCharacter.fsm.SetBool("Moving", false);

                casterCharacter.transform.LookAt(targetPoint);
            }
            else
            {
                casterCharacter.transform.LookAt(targetPoint);
            }

            // Play Animation and wait for Cast Point

            // TODO: refactor
            if (string.IsNullOrEmpty(abilityData.Animation) == false)
                {
                    //yield return new WaitForEndOfFrame();  // Buffer for sync
                    //ownerProfile.fsm.SetTrigger(abilityData.Animation);
                    //yield return new WaitForSeconds(abilityData.AnimCastPoint);
                }

            if (Channelled)
            {
                int channelTimer = 0;
                yield return OnSpellStart();
                while (channelTimer < abilityData.Duration)
                {
                    yield return OnChannelSucceed();
                    yield return new WaitForSeconds(1);
                    channelTimer += 1;
                }
            }
            else
            {
                yield return OnSpellStart();
            }

            yield return End();
        }
    }


    IEnumerator OnSpellStart()
    {
        if (abilityData.Name == "Move")
        {
            if (UnityEngine.Random.Range(0, 5) < 2)
            {
                //casterCharacter.photonView.RPC("PlayVoice", GameManager.Instance.PlayerRegistry[owner.name], ResourceManager.MOVE);
            }
        }
    //REFACTOR: either combine move and others
        else
        {
            if (UnityEngine.Random.Range(0, 5) < 2)
            {
                //casterCharacter.photonView.RPC("PlayVoice", GameManager.Instance.PlayerRegistry[owner.name], ResourceManager.SPELL_CAST);
            }
        }

        StartCooldownTimer();
        isCast = true;

        yield return PerformActions(EventRegister[EventAbility.OnSpellStart]);
    }

    IEnumerator OnChannelSucceed()
    {
        targetCharacters.Clear();
        yield return PerformActions(EventRegister[EventAbility.OnChannelSucceed]);
    }

    public IEnumerator End()
    {
        Debug.Log("Ability ended");

        targetCharacters.Clear();
        isRunning = false;
        isCast = false;

        Debug.Log("Ability has " + targetCharacters.Count + " targets");

        foreach (var action in EventRegister[EventAbility.OnSpellStart])
        {
            yield return action.Reset();
        }

        yield return null;
    }

    public void OnProjectileHit(CharacterSystem targetCharacter)
    {
        Debug.Log("On hit");

        if (EventRegister[EventAbility.OnProjectileHitUnit] != null)
        {
            foreach (var action in EventRegister[EventAbility.OnProjectileHitUnit])
            {
                selfCharacter.StartCoroutine(action.Excecute(this, indicator, selfCharacter, targetCharacter));
            }
        }
    }

    IEnumerator PerformActions(List<BaseAction> actions)
    {
        yield return null;


        foreach (var action in actions)
        {
            Debug.Log("Performing action: " + action.data.Type.ToString());

            switch (action.data.Target)
            {
                case Target.Caster:
                    {
                        Debug.Log("Casting self");
                        selfCharacter.StartCoroutine(action.Excecute(this, indicator, selfCharacter, null));
                        break;
                    }

                case Target.Target:
                    {
                        if (action.data.MultipleTargets)
                        {
                            foreach (var targetCharacter in targetCharacters)
                            {
                                yield return action.Excecute(this, indicator, selfCharacter, targetCharacter);
                            }
                        }
                        else
                        {
                            yield return action.Excecute(this, indicator, selfCharacter, targetCharacters[0]);
                        }
                        break;
                    }

                case Target.Point:
                    {
                        if (action.data.MultipleTargets)
                        {
                            foreach (var target in targetCharacters)
                            {
                                yield return action.Excecute(this, indicator, selfCharacter, null);
                            }
                        }
                        else
                        {
                            yield return action.Excecute(this, indicator, selfCharacter, null);
                        }
                        break;
                    }
            }
        }

        yield return null;
    }

    public object Clone()
    {
        //throw new NotImplementedException();
        var ability = new Ability(this.abilityData)
        {
            EventRegister = new Dictionary<EventAbility, List<BaseAction>>()
        };

        foreach (var entry in EventRegister)
        {
            ability.EventRegister[entry.Key] = new List<BaseAction>();
            foreach (var action in entry.Value)
            {
                ability.EventRegister[entry.Key].Add(action.Clone());
            }
        }

        return ability;
    }

    private void StartCooldownTimer()
    {
        CurrentCooldown = abilityData.Cooldown;
    }

    //public Vector3 GetTargetPoint()
    //{
    //    return indicator * abilityData.RangeCast.Value;
    //}

}
