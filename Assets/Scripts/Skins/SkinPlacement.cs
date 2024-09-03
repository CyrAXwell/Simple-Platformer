using System;
using UnityEngine;
using Zenject;

public class SkinPlacement : MonoBehaviour
{
    [SerializeField] private Transform skinsParent;

    private ShopContent _shopContent;
    private TrailSkins _initTrailSkinType;
    private OrbSkins _initOrbSkinType;
    private GameObject _currentTrailSkin;
    private GameObject _currentOrbSkin;
    private TrailSkinItem _selectedTrailSkin;
    private OrbSkinItem _selectedOrbSkin;

    [Inject]
    private void Consruct(IPersistentData persistentPlayerData)
    {
        _initTrailSkinType = persistentPlayerData.PlayerData.SelectedTrailSkin;
        _initOrbSkinType = persistentPlayerData.PlayerData.SelectedOrbSkin; 
    }

    public void Initialize(ShopContent contentItems)
    {
        _shopContent = contentItems;

        foreach (var item in _shopContent.TrailSkinItems)
        {
            if (item.SkinType == _initTrailSkinType)
            {
                _currentTrailSkin = Instantiate(item.Model, skinsParent);
                _selectedTrailSkin = item;
            }
        }

        foreach (var item in _shopContent.OrbSkinItems)
        {
            if (item.SkinType == _initOrbSkinType)
            {
                _currentOrbSkin = Instantiate(item.Model, skinsParent);
                _selectedOrbSkin = item;
            }
        }
    }

    public void ResetSkins()
    {
        if (_shopContent != null)
        {
            foreach (var item in _shopContent.TrailSkinItems)
            {
                if (item.SkinType == _selectedTrailSkin.SkinType)
                {
                    if (_currentTrailSkin != null)
                        Destroy(_currentTrailSkin.gameObject);
   
                    _currentTrailSkin = Instantiate(item.Model, skinsParent);
                }
            }

            foreach (var item in _shopContent.OrbSkinItems)
            {
                if (item.SkinType == _selectedOrbSkin.SkinType)
                {
                    if (_currentOrbSkin != null)
                        Destroy(_currentOrbSkin.gameObject);

                    _currentOrbSkin = Instantiate(item.Model, skinsParent);
                }
            }
        }
    }

    public void SetSkin(ShopItem shopItem)
    {
        switch (shopItem)
        {
            case TrailSkinItem trailSkinItem:
                _selectedTrailSkin = (TrailSkinItem)shopItem;
                break;
            
            case OrbSkinItem orbSkinItem:
                _selectedOrbSkin = (OrbSkinItem)shopItem;
                break;
            default:
                throw new ArgumentException(nameof(shopItem));
        }
    }


    public void InstantiateModel(ShopItem shopItem)
    {
        switch (shopItem)
        {
            case TrailSkinItem trailSkinItem:
                if (_currentTrailSkin != null)
                    Destroy(_currentTrailSkin.gameObject);    

                _currentTrailSkin = Instantiate(shopItem.Model, skinsParent);
                break;
            
            case OrbSkinItem orbSkinItem:
                if (_currentOrbSkin != null)
                    Destroy(_currentOrbSkin.gameObject); 

                _currentOrbSkin = Instantiate(shopItem.Model, skinsParent);
                break;
            default:
                throw new ArgumentException(nameof(shopItem));
        }
    }
}
