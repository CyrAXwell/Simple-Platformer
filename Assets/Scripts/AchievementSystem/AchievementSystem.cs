using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private List<Achievement> achievements;
    [SerializeField] private AchievementComponent achievementPrefab;
    [SerializeField] private Shop shop;
    [SerializeField] private AchievementProgress achievementProgress;
    [SerializeField] private AchievementManager achievementManager;

    private IPersistentData _persistentData;
    private Wallet _wallet;
    private int _id = 0;
    private IDataProvider _dataProvider;
    private List<AchievementComponent> _achievementComponents = new List<AchievementComponent>();
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [Inject]
    private void Construct(IPersistentData persistentData, Wallet wallet, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _wallet = wallet;
        _dataProvider = dataProvider;

        shop.BuySkin += UpdateAchievements;

        foreach (Achievement achievement in achievements)
        {
            
            AchievementComponent achievementComponent = Instantiate(achievementPrefab, content);
            _achievementComponents.Add(achievementComponent);

            persistentData.PlayerData.CreateAchivement(_id);
            achievementComponent.Initialize(achievement, _persistentData, _wallet, _id, _dataProvider);
            achievementComponent.GetRewardClick += OnGetRewardClick;
            _id ++; 
        }

        achievementProgress.Initialize(_persistentData, _achievementComponents);
        achievementManager.Initialize(_achievementComponents, achievements);
    }

    private void UpdateAchievements(AchievementTypes type)
    {
        achievementProgress.DisplayProgress(_achievementComponents);
        achievementManager.LoadData(_persistentData);
        achievementManager.CheckAchievements();

        foreach (AchievementComponent achievement in _achievementComponents)
        {
            if (achievement.Type == type)
            {
                achievement.UpdateValue();
            }
        }
    }

    private void OnGetRewardClick() => _audioManager.PlaySFX(_audioManager.GetReward);

    private void OnDestroy()
    {
        shop.BuySkin -= UpdateAchievements;

        foreach (AchievementComponent achievement in _achievementComponents)
        {
            achievement.GetRewardClick -= OnGetRewardClick;
        }
    }
    
}
