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
    private string _sceneName;

    public static AchievementManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }    

    public void Initialize(List<AchievementComponent> achievementComponents, List<Achievement> achievements)
    {
        _achievementComponents = achievementComponents;
        _achievements = achievements;
    }

    public void CheckAchievements()
    {
        if (_sceneName == "GameScene")
        {
            _levelFinish.LevelComplete -= CheckAchievements;
        }

        float counter = 0;

        foreach (AchievementComponent achievement in _achievementComponents)
        {
            if (achievement.CheckValue())
            {
                DisplayAchievement(achievement.Id, counter);
                counter ++;
            }    
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _sceneName = scene.name;
        if (_sceneName == "GameScene")
        {
            _levelFinish = GameObject.Find("Trigger").GetComponent<LevelFinish>();
            _levelFinish.LevelComplete += CheckAchievements;  
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
