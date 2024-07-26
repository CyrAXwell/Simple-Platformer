using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView trailSkinItemPrefab;
    [SerializeField] private ShopItemView orbSkinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(trailSkinItemPrefab, orbSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _trailSkinItemPrefab;
        private ShopItemView _orbSkinItemPrefab;

        public ShopItemVisitor(ShopItemView trailSkinItemPrefab, ShopItemView orbSkinItemPrefab)
        {
            _trailSkinItemPrefab = trailSkinItemPrefab;
            _orbSkinItemPrefab = orbSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

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

        public void Visit(TrailSkinItem trailSkinItem) => Prefab = _trailSkinItemPrefab;

        public void Visit(OrbSkinItem orbSkinItem) => Prefab = _orbSkinItemPrefab;
    }
}
