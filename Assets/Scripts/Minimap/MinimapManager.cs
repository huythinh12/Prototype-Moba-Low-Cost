using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.System;

[DefaultExecutionOrder(100)]
public class MinimapManager : MonoBehaviour
{
    static readonly float SizeIconHero = 12f;
    static readonly float SizeIconLegion = 8f;
    static readonly float SizeIconHeroBorder = 21f;

    static readonly float SizeIconSmallCreep = 10f;
    static readonly float SizeIconMediumCreep = 12f;
    static readonly float SizeIconLargeCreep = 14f;

    static readonly float SizeIconTower = 12f;

    public static MinimapManager Instance { get; private set; }

    [SerializeField] Sprite iconBorderRed;
    [SerializeField] Sprite iconBorderBlue;

    [SerializeField] GameObject positionZoomRealMap;
    [SerializeField] RectTransform positionZoomMiniMap;
    [SerializeField] RectTransform iconGameobject;
    [SerializeField] Transform chacracter;
    [SerializeField] Transform backgroundMiniMap;


    double factorZoom;
    public static Dictionary<CharacterSystem, RectTransform> iconMinimaps = new Dictionary<CharacterSystem, RectTransform>();
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    void Start()
    {
        factorZoom = positionZoomRealMap.transform.localPosition.magnitude / positionZoomMiniMap.localPosition.magnitude;

    }
    RectTransform CreateMinimapIcon(CharacterSystem characterSystem)
    {
        
        // 
        string iconName = string.Format("IconHero: {0} ({1})",characterSystem.GetProfile.Name, characterSystem.GetProfile.GetTeamCharacter);

        RectTransform iconRectrasnfom = new GameObject(iconName, typeof(RectTransform), typeof(Image)).GetComponent<RectTransform>();

        var iconImage = iconRectrasnfom.GetComponent<Image>();
        iconImage.sprite = characterSystem.GetProfile.IconMinimap;

       
        iconRectrasnfom.SetParent(backgroundMiniMap);
        iconRectrasnfom.localPosition = Vector3.zero;
        iconRectrasnfom.localScale = Vector3.one;


        switch (characterSystem.GetProfile.GetTypeCharacter)
        {
            case TypeCharacter.Hero:
                string iconBorderName = string.Format("Icon Border: {0} ({1})", characterSystem.GetProfile.Name, characterSystem.GetProfile.GetTeamCharacter);
                RectTransform iconborderRectrasnfom = new GameObject(iconBorderName, typeof(RectTransform), typeof(Image)).GetComponent<RectTransform>();

                var iconborderImage = iconborderRectrasnfom.GetComponent<Image>();
                iconborderImage.sprite = characterSystem.GetProfile.GetTeamCharacter == TeamCharacter.Blue ? iconBorderBlue : iconBorderRed;

                iconborderRectrasnfom.SetParent(iconRectrasnfom.transform);
                iconborderRectrasnfom.localPosition = Vector3.zero;
                iconborderRectrasnfom.localScale = Vector3.one;

                iconRectrasnfom.sizeDelta = new Vector2(SizeIconHero, SizeIconHero);
                iconRectrasnfom.SetSiblingIndex(100);
                iconborderRectrasnfom.sizeDelta = new Vector2(SizeIconHeroBorder, SizeIconHeroBorder);

                break;
            case TypeCharacter.Legion:
                iconRectrasnfom.sizeDelta = new Vector2(SizeIconLegion, SizeIconLegion);
                iconRectrasnfom.SetSiblingIndex(1);

                break;
            case TypeCharacter.Tower:
                iconRectrasnfom.sizeDelta = new Vector2(SizeIconTower, SizeIconTower);
                break;
            case TypeCharacter.SmallCreep:
                iconRectrasnfom.sizeDelta = new Vector2(SizeIconSmallCreep, SizeIconSmallCreep);
                break;
            case TypeCharacter.MediumCreep:
                iconRectrasnfom.sizeDelta = new Vector2(SizeIconMediumCreep, SizeIconMediumCreep);
                break;
            case TypeCharacter.LargeCreep:
                iconRectrasnfom.sizeDelta = new Vector2(SizeIconLargeCreep, SizeIconLargeCreep);
                break;
            default:
                break;
        }

        return iconRectrasnfom;
    }


    private static RectTransform GetIconRectTransfom(CharacterSystem characterSystem)
    {
        RectTransform iconRectTransform;
        iconMinimaps.TryGetValue(characterSystem, out iconRectTransform);
        return iconRectTransform;
    }
    public void InitializeIcon(CharacterSystem characterSystem)
    {
        iconMinimaps.Add(characterSystem, CreateMinimapIcon(characterSystem));
    }

    public void TurnOnIcon(CharacterSystem characterSystem)
    {
        GetIconRectTransfom(characterSystem).gameObject.SetActive(true);

    }
    public void TurnOffIcon(CharacterSystem characterSystem)
    {
        GetIconRectTransfom(characterSystem).gameObject.SetActive(false);
      
    }

    public void UpdatePosition(CharacterSystem characterSystem)
    {
        GetIconTrasnform(characterSystem).localPosition = new Vector3((float)(characterSystem.transform.localPosition.x / factorZoom),(float)(characterSystem.transform.localPosition.z / factorZoom));
    }


    public RectTransform GetIconTrasnform(CharacterSystem characterSystem)
    {
        RectTransform iconRectTransform;
        iconMinimaps.TryGetValue(characterSystem, out iconRectTransform);
        return iconRectTransform;
    }
}
