using CharacterMechanism.System;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1000)]
public class KDAUI : MonoBehaviour
{
    static readonly string KDAPrefabPath = "Prefabs/KDA/KDA Player";

    CharacterSystem characterSystem;

    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] TextMeshProUGUI assistText;

    static public KDAUI Create(CharacterSystem characterSystem, Canvas canvasParent)
    {
        KDAUI kdaUI = Instantiate(Resources.Load<KDAUI>(KDAPrefabPath), canvasParent.transform);
        characterSystem.OnKDAChange += kdaUI.UpdateKDAText;
        return kdaUI;
    }

    public void UpdateKDAText(CharacterSystem characterSystem)
    {
        Debug.Log("Update KDA Text!");

        killText.text = characterSystem.GetHistory.Kill.ToString();
        deathText.text = characterSystem.GetHistory.Death.ToString();
        assistText.text = characterSystem.GetHistory.Assist.ToString();
    }
}
