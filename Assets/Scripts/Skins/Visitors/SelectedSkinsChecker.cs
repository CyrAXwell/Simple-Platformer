
public class SelectedSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;


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

    public void Visit(TrailSkinItem trailSkinItem) => IsSelected =_persistentData.PlayerData.SelectedTrailSkin == trailSkinItem.SkinType;

    public void Visit(OrbSkinItem orbSkinItem) => IsSelected =_persistentData.PlayerData.SelectedOrbSkin == orbSkinItem.SkinType;
}
