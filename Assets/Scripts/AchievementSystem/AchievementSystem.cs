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

    private int _id = 0;
    private List<AchievementComponent> _achievementComponents = new List<AchievementComponent>();
    private DiContainer _diContainer;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [Inject]
    private void Construct(IPersistentData persistentData, DiContainer diContainer)
    {
        _diContainer = diContainer;

        shop.BuySkin += UpdateAchievements;

        foreach (Achievement achievement in achievements)
        {
            AchievementComponent achievementComponent = _diContainer.InstantiatePrefab(achievementPrefab, content).GetComponent<AchievementComponent>();
            _achievementComponents.Add(achievementComponent);

            persistentData.PlayerData.CreateAchivement(_id);
            achievementComponent.Initialize(achievement, _id);
            achievementComponent.GetRewardClick += OnGetRewardClick;
            _id ++; 
        }

        achievementProgress.Initialize(_achievementComponents);
        achievementManager.Initialize(_achievementComponents, achievements);
    }

    private void UpdateAchievements(AchievementTypes type)
    {
        achievementProgress.DisplayProgress(_achievementComponents);
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
