using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class AbilityDatabase : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, Ability> Database = new Dictionary<string, Ability>();
    public static AbilityDatabase Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAbilityData();
    }

    void LoadAbilityData()
    {
        Debug.Log("Hi!");

        var abilities = Resources.LoadAll<DataDrivenAbility>("Abilities") as DataDrivenAbility[];

        for (int i = 0; i < abilities.Length; i++)
        {
            DataDrivenAbility rawAbility = abilities[i];
            Ability ability = DataDrivenAbility.Parse(rawAbility);
            Database[ability.abilityData.Name] = ability;
        }
    }

    public Ability GetAbility(string abilityName)
    {
        if (Database.ContainsKey(abilityName))
        {
            return Database[abilityName].Clone() as Ability;
        }

        throw new System.Exception("Ability: " + abilityName + " doesn't exist");
    }
}