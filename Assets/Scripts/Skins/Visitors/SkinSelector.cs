
public class SkinSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;
    
    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

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

    public void Visit(TrailSkinItem trailSkinItem) => _persistentData.PlayerData.SelectedTrailSkin = trailSkinItem.SkinType;

    public void Visit(OrbSkinItem orbSkinItem) => _persistentData.PlayerData.SelectedOrbSkin = orbSkinItem.SkinType;
}
