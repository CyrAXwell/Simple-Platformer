using UnityEngine;

[CreateAssetMenu(fileName = "OrbSkinItem", menuName = "Shop/OrbSkinItem") ]
public class OrbSkinItem : ShopItem
{
    [field: SerializeField] public OrbSkins SkinType { get; private set; }
}
