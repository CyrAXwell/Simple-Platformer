using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbFactory", menuName ="Player/OrbFactory")]
public class OrbFactory : ScriptableObject
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

    public GameObject Get(OrbSkins skinType, Transform player)
    {
        GameObject instance = Instantiate(GetPrefab(skinType), player);

        return instance;
    } 

    public GameObject GetPrefab(OrbSkins skinType)
    {
        switch(skinType)
        {
            case OrbSkins.DefaultWhite:
                return defaultWhite;
            case OrbSkins.DefaultBlue:
                return defaultBlue;
            case OrbSkins.DefaultPurple:
                return defaultPurple;
            case OrbSkins.DefaultRed:
                return defaultRed;
            case OrbSkins.DefaultGreen:
                return defaultGreen;
            
            case OrbSkins.MagicWhite:
                return magicWhite;
            case OrbSkins.MagicBlue:
                return magicBlue;
            case OrbSkins.MagicPurple:
                return magicPurple;
            case OrbSkins.MagicRed:
                return magicRed;
            case OrbSkins.MagicGreen:
                return magicGreen;
            
            case OrbSkins.NeonWhite:
                return neonWhite;
            case OrbSkins.NeonBlue:
                return neonBlue;
            case OrbSkins.NeonPurple:
                return neonPurple;
            case OrbSkins.NeonRed:
                return neonRed;
            case OrbSkins.NeonGreen:
                return neonGreen;

            case OrbSkins.ShadowWhite:
                return shadowWhite;
            case OrbSkins.ShadowBlue:
                return shadowBlue;
            case OrbSkins.ShadowPurple:
                return shadowPurple;
            case OrbSkins.ShadowRed:
                return shadowRed;
            case OrbSkins.ShadowGreen:
                return shadowGreen;

            case OrbSkins.ElectricWhite:
                return electricWhite;
            case OrbSkins.ElectricBlue:
                return electricBlue;
            case OrbSkins.ElectricPurple:
                return electricPurple;
            case OrbSkins.ElectricRed:
                return electricRed;
            case OrbSkins.ElectricGreen:
                return electricGreen;
            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}