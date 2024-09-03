using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool Isopened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem)
    {
        switch (shopItem)
        {
            case TrailSkinItem trailSkinItem:
                Visit(trailSkinItem);
                break;  
            case OrbSkinItem orbSkinItem:
                Visit(orbSkinItem);
                break; 
        }
    }

    public void Visit(TrailSkinItem trailSkinItem) => Isopened = _persistentData.PlayerData.OpenTrailSkins.Contains(trailSkinItem.SkinType);

    public void Visit(OrbSkinItem orbSkinItem) => Isopened = _persistentData.PlayerData.OpenOrbSkins.Contains(orbSkinItem.SkinType);

}
