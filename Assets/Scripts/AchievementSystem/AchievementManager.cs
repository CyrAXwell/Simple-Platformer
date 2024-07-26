using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private AchievementDisplay displayPrefab;

    private List<AchievementComponent> _achievementComponents;
    private List<Achievement> _achievements;
    private LevelFinish _levelFinish;
    private IPersistentData _persistentData;
    private string sceneName;
    private AudioManager _audioManager;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(List<AchievementComponent> achievementComponents, List<Achievement> achievements)
    {
        _achievementComponents = achievementComponents;
        _achievements = achievements;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;
        if (sceneName == "GameScene")
        {
            _levelFinish = GameObject.Find("Trigger").GetComponent<LevelFinish>();
            _levelFinish.LevelComplete += CheckAchievements;  
            _persistentData = _levelFinish.PersistentData;
        } 
    }

    public void LoadData(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    public void CheckAchievements()
    {
        if (sceneName == "GameScene")
        {
            _levelFinish.LevelComplete -= CheckAchievements;
        }

        float counter = 0;

        foreach (AchievementComponent achievement in _achievementComponents)
        {
            if (achievement.CheckValue(_persistentData))
            {
                DisplayAchievement(achievement.Id, counter);
                counter ++;
            }    
        }
    }

    private void DisplayAchievement(int Id, float counter)
    {
        AchievementDisplay achievementDisplay = Instantiate(displayPrefab);
        achievementDisplay.Initialize(_achievements[Id]);

        float interval = 2.3f;
        float delay = interval * counter;
        achievementDisplay.Display.DOAnchorPos(new Vector2(0, 0), 0.5f).SetDelay(delay).SetUpdate(true).SetLink(achievementDisplay.gameObject); //OnStart(GetAchievement)
        achievementDisplay.Display.DOAnchorPos(new Vector2(-722.5f, 0), 0.3f).SetDelay(delay + 2f).SetUpdate(true).SetLink(achievementDisplay.gameObject);
        Destroy(achievementDisplay.gameObject, 2.5f + delay);
    }
}
