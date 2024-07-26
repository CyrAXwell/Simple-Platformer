using System;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    public event Action LevelComplete;
    
    [SerializeField] private StarCollector starCollector;
    [SerializeField] private TMP_Text text;
    [SerializeField] private SpriteRenderer finishLine;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentPlayerData;
    private Wallet _wallet;
    private AudioManager _audioManager;
    private bool _isLevelSkip;
    private YandexSDK _yandexSDK;

    public IPersistentData PersistentData => _persistentPlayerData;
    public bool IsLevelSkip => _isLevelSkip;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Initialize(IDataProvider dataProvider, IPersistentData persistentPlayerData)
    {
        _dataProvider = dataProvider;
        _persistentPlayerData = persistentPlayerData;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }   

    public void CompleteLevel()
    {
        _audioManager.PlaySFX(_audioManager.LevelComplete);
        
        _yandexSDK = GameObject.FindGameObjectWithTag("ySDK").GetComponent<YandexSDK>();
        _yandexSDK.ShowAdv();

        int unlockedLevels = _persistentPlayerData.PlayerData.UnlockedLevels;

        if (LevelManager.currentLevel == unlockedLevels && unlockedLevels < LevelManager.maxLevels)
        {
            _persistentPlayerData.PlayerData.UnlockedLevels += 1;
            _persistentPlayerData.PlayerData.AddLevelsStars(unlockedLevels + 1, null);
        }
        
        if (_persistentPlayerData.PlayerData.CompletedLevels < LevelManager.currentLevel)
            _persistentPlayerData.PlayerData.CompletedLevels += 1;

        int stars = 0;

        if( _persistentPlayerData.PlayerData.LevelsStars.ToList()[LevelManager.currentLevel-1] != null)
            stars = _persistentPlayerData.PlayerData.LevelsStars.ToList()[LevelManager.currentLevel-1].Count();

        int currentStars = starCollector.GetStarsNum();
        if (currentStars > stars)
        {
            _persistentPlayerData.PlayerData.AddLevelsStars(LevelManager.currentLevel, starCollector.GetStarsList());
            _wallet = new Wallet(_persistentPlayerData);
            _wallet.AddCoins(currentStars - stars);
        }
        
        FadeFinishLine();
        
        _dataProvider.Save();

        LevelComplete?.Invoke();
    }

    public void SkipLevel()
    {
        _persistentPlayerData.PlayerData.UnlockedLevels += 1;
        _persistentPlayerData.PlayerData.AddLevelsStars(_persistentPlayerData.PlayerData.UnlockedLevels, null);

        _persistentPlayerData.PlayerData.CompletedLevels += 1;

        _dataProvider.Save();
        _isLevelSkip = true;
        LevelComplete?.Invoke();
    }

    public bool IsCompletedLevel()
    {
        return _persistentPlayerData.PlayerData.CompletedLevels >= LevelManager.currentLevel;
    } 
    

    private void FadeFinishLine()
    {
        finishLine.DOFade(0f, 1f).SetUpdate(true);
        text.DOFade(0f, 1f).SetUpdate(true);
    }
}
