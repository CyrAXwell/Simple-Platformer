using UnityEngine;

[CreateAssetMenu(fileName = "TrailSkinItem", menuName = "Shop/TrailSkinItem") ]
public class TrailSkinItem : ShopItem
{
    [field: SerializeField] public TrailSkins SkinType { get; private set; }
}
