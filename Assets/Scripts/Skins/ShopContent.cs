using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<TrailSkinItem> trailSkinItems;
    [SerializeField] private List<OrbSkinItem> orbSkinItems;

    public IEnumerable<TrailSkinItem> TrailSkinItems => trailSkinItems;
    public IEnumerable<OrbSkinItem> OrbSkinItems => orbSkinItems;
}
