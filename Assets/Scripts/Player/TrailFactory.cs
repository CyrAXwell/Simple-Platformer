using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TrailFactory", menuName ="Player/TrailFactory")]
public class TrailFactory : ScriptableObject
{
    [SerializeField] private GameObject defaultWhite;
    [SerializeField] private GameObject defaultBlue;
    [SerializeField] private GameObject defaultPurple;
    [SerializeField] private GameObject defaultRed;
    [SerializeField] private GameObject defaultGreen;

    [SerializeField] private GameObject magicWhite;
    [SerializeField] private GameObject magicBlue;
    [SerializeField] private GameObject magicPurple;
    [SerializeField] private GameObject magicRed;
    [SerializeField] private GameObject magicGreen;

    [SerializeField] private GameObject neonWhite;
    [SerializeField] private GameObject neonBlue;
    [SerializeField] private GameObject neonPurple;
    [SerializeField] private GameObject neonRed;
    [SerializeField] private GameObject neonGreen;

    [SerializeField] private GameObject shadowWhite;
    [SerializeField] private GameObject shadowBlue;
    [SerializeField] private GameObject shadowPurple;
    [SerializeField] private GameObject shadowRed;
    [SerializeField] private GameObject shadowGreen;

    [SerializeField] private GameObject electricWhite;
    [SerializeField] private GameObject electricBlue;
    [SerializeField] private GameObject electricPurple;
    [SerializeField] private GameObject electricRed;
    [SerializeField] private GameObject electricGreen;

    public GameObject Get(TrailSkins skinType, Transform player)
    {
        GameObject instance = Instantiate(GetPrefab(skinType), player);

        return instance;
    } 

    public GameObject GetPrefab(TrailSkins skinType)
    {
        switch(skinType)
        {
            case TrailSkins.DefaultWhite:
                return defaultWhite;
            case TrailSkins.DefaultBlue:
                return defaultBlue;
            case TrailSkins.DefaultPurple:
                return defaultPurple;
            case TrailSkins.DefaultRed:
                return defaultRed;
            case TrailSkins.DefaultGreen:
                return defaultGreen;
            
            case TrailSkins.MagicWhite:
                return magicWhite;
            case TrailSkins.MagicBlue:
                return magicBlue;
            case TrailSkins.MagicPurple:
                return magicPurple;
            case TrailSkins.MagicRed:
                return magicRed;
            case TrailSkins.MagicGreen:
                return magicGreen;
            
            case TrailSkins.NeonWhite:
                return neonWhite;
            case TrailSkins.NeonBlue:
                return neonBlue;
            case TrailSkins.NeonPurple:
                return neonPurple;
            case TrailSkins.NeonRed:
                return neonRed;
            case TrailSkins.NeonGreen:
                return neonGreen;

            case TrailSkins.ShadowWhite:
                return shadowWhite;
            case TrailSkins.ShadowBlue:
                return shadowBlue;
            case TrailSkins.ShadowPurple:
                return shadowPurple;
            case TrailSkins.ShadowRed:
                return shadowRed;
            case TrailSkins.ShadowGreen:
                return shadowGreen;

            case TrailSkins.ElectricWhite:
                return electricWhite;
            case TrailSkins.ElectricBlue:
                return electricBlue;
            case TrailSkins.ElectricPurple:
                return electricPurple;
            case TrailSkins.ElectricRed:
                return electricRed;
            case TrailSkins.ElectricGreen:
                return electricGreen;
            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
