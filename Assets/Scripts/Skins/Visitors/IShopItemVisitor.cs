
public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    
    void Visit(TrailSkinItem trailSkinItem);

    void Visit(OrbSkinItem orbSkinItem);
}
