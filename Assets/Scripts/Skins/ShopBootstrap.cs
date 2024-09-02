using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop shop;

    public void Awake()
    {
        shop.SkinPlacementInitialize();
    }
}
