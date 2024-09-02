using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent contentItems;
    [SerializeField] private ShopCategoryButton trailSkinsButton;
    [SerializeField] private ShopCategoryButton orbSkinsButton;
    [SerializeField] private BuyButton buyButton;
    [SerializeField] private Button selectionButton;
    [SerializeField] private Image selectedText;
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private SkinPlacement skinPlacement;

    private IDataProvider _dataProvider;
    private ShopItemView _previewedItem;
    private Wallet _wallet;
    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinsChecker _selectedSkinsChecker;
    private AudioManager _audioManager;
    
    public event Action<AchievementTypes> BuySkin;

    private void OnEnable()
    {
        trailSkinsButton.Click += OnTrailSkinsbuttonClick;
        orbSkinsButton.Click += OnOrbSkinsbuttonClick;

        shopPanel.ItemViewClicked += OnItemViewClicked;

        buyButton.Click += OnBuyButtonClick;
        selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        trailSkinsButton.Click -= OnTrailSkinsbuttonClick;
        orbSkinsButton.Click -= OnOrbSkinsbuttonClick;

        shopPanel.ItemViewClicked -= OnItemViewClicked;

        buyButton.Click -= OnBuyButtonClick;
        selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    [Inject]
    private void Construct(IDataProvider dataProvider, Wallet wallet, OpenSkinsChecker openSkinsChecker, SelectedSkinsChecker selectedSkinsChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker)
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        _wallet = wallet;
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinsChecker = selectedSkinsChecker;
        _skinSelector = skinSelector;
        _skinUnlocker = skinUnlocker;

        _dataProvider = dataProvider;

        shopPanel.Initialize(openSkinsChecker, selectedSkinsChecker);
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        _previewedItem = item;
        skinPlacement.InstantiateModel(_previewedItem.Item);

        _openSkinsChecker.Visit(_previewedItem.Item);

        if (_openSkinsChecker.Isopened)
        {
            _selectedSkinsChecker.Visit(_previewedItem.Item);

            if (_selectedSkinsChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewedItem.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        if (_wallet.IsEnough(_previewedItem.Price))
        {
            _audioManager.PlaySFX(_audioManager.BuyButton);          
            
            _wallet.Spend(_previewedItem.Price);

            _skinUnlocker.Visit(_previewedItem.Item);

            SelectSkin();

            _previewedItem.Unlock();

            _dataProvider.Save();

            switch (_previewedItem.Item)
            {
                case TrailSkinItem trailSkins:
                    BuySkin?.Invoke(AchievementTypes.TrailSkins);
                    break;
                case OrbSkinItem orbSkinItem:
                    BuySkin?.Invoke(AchievementTypes.OrbSkins);
                    break;
            }
        }
    }

    private void OnSelectionButtonClick()
    {
        _audioManager.PlaySFX(_audioManager.BuyButton);

        SelectSkin();

        _dataProvider.Save();
    }

    private void OnTrailSkinsbuttonClick()
    {
        trailSkinsButton.Select();
        orbSkinsButton.Unselect();
        shopPanel.Show(contentItems.TrailSkinItems.Cast<ShopItem>());

        skinPlacement.ResetSkins();
    }

    private void OnOrbSkinsbuttonClick()
    {
        trailSkinsButton.Unselect();
        orbSkinsButton.Select();
        shopPanel.Show(contentItems.OrbSkinItems.Cast<ShopItem>());

        skinPlacement.ResetSkins();
    }

    private void SelectSkin()
    {
        _skinSelector.Visit(_previewedItem.Item);
        shopPanel.Select(_previewedItem);
        ShowSelectedText();

        skinPlacement.SetSkin(_previewedItem.Item);
    }

    private void ShowSelectionButton()
    {
        selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }

    private void ShowSelectedText()
    {
        selectedText.gameObject.SetActive(true);
        HideSelectionButton();
        HideBuyButton();
    }

    private void ShowBuyButton(int price)
    {
        buyButton.gameObject.SetActive(true);
        buyButton.UpdateText(price);

        if (_wallet.IsEnough(price))
            buyButton.Unlock();
        else
            buyButton.Lock();

        HideSelectedText();
        HideSelectionButton();
    }

    private void HideBuyButton() => buyButton.gameObject.SetActive(false);

    private void HideSelectionButton() => selectionButton.gameObject.SetActive(false);

    private void HideSelectedText() => selectedText.gameObject.SetActive(false);

    public void SkinPlacementInitialize()
    {
        skinPlacement.Initialize(contentItems);
        OnTrailSkinsbuttonClick();
    }
        

}
