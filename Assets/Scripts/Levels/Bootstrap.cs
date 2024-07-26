using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TrailFactory trailFactory;
    [SerializeField] private OrbFactory orbFactory;
    [SerializeField] private GameSceneLevelManager gameSceneLevelManager;
    [SerializeField] private GamePlayHUD gamePlayHUD;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;

    private void Awake()
    {
        gamePlayHUD.transform.parent.gameObject.SetActive(true);
        
        InitializeData();

        GetSkins();
        
        InitializeLevel();

        gamePlayHUD.ShowSkipButton();
    }

    private void GetSkins()
    {
        GameObject trail = trailFactory.Get(_persistentPlayerData.PlayerData.SelectedTrailSkin, player);
        GameObject orb = orbFactory.Get(_persistentPlayerData.PlayerData.SelectedOrbSkin, player);
    }

    private void InitializeData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }

    private void InitializeLevel()
    {
        gameSceneLevelManager.Initialize(_dataProvider, _persistentPlayerData);
    }
}