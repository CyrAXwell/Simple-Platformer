using UnityEngine;

public class ShopBootstrap : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private WalletView walletView;
    [SerializeField] private SkinPlacement skinPlacement;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private AchievementSystem achievementSystem;
    [SerializeField] private YandexSDK yandexSDK;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private Wallet _wallet;

    public void Awake()
    {
        InitializeData();

        InitializeWallet();

        InitializeShop();

        InitializeAchievement();
    }

    private void InitializeData()
    {
        _persistentPlayerData = new  PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);
        
        LoadDataOrInit();
        
        levelManager.Initialize(_persistentPlayerData);
    }

    private void InitializeWallet()
    {
        _wallet = new Wallet(_persistentPlayerData);
        
        walletView.Initialize(_wallet);
    }

    private void InitializeShop()
    {
        OpenSkinsChecker openSkinsChecker = new OpenSkinsChecker(_persistentPlayerData);
        SelectedSkinsChecker selectedSkinsChecker = new SelectedSkinsChecker(_persistentPlayerData);
        SkinSelector skinSelector = new SkinSelector(_persistentPlayerData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentPlayerData);

        shop.SkinPlacementInit();
        InitializeSkinPlacement();

        shop.Initialize(_dataProvider, _wallet, openSkinsChecker, selectedSkinsChecker, skinSelector, skinUnlocker);
    }

    private void InitializeSkinPlacement()
    {
        skinPlacement.Initialize(_persistentPlayerData.PlayerData.SelectedTrailSkin, _persistentPlayerData.PlayerData.SelectedOrbSkin);
    }

    private void InitializeAchievement()
    {
        achievementSystem.Initialize(_persistentPlayerData, _wallet, _dataProvider);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }
}
