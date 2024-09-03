using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelSelector> levelSelectors;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private UIManager uIManager;
    
    private IPersistentData _persistentData;
    private AudioManager _audioManager;

    public static int maxLevels = 40;
    public static int currentLevel = 1;

    [Inject]
    private void Construct(IPersistentData persistentData)
    {
        _persistentData = persistentData;
        maxLevels = levelSelectors.Count;
        for (int i = 0; i < levelSelectors.Count(); i++)
            levelSelectors[i].Initialize(i+1);

        LoadlevelPanel();
    }

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); 
    }

    public void StartLevel(LevelSelector level)
    {
        _audioManager.PlaySFX(_audioManager.ButtonClick);

        currentLevel = level.LevelIndex;
        sceneTransition.EndSceneTransition("GameScene");
    }

    public void OnPlayButton()
    {
        if (_persistentData.PlayerData.UnlockedLevels < maxLevels)
        {
            currentLevel = _persistentData.PlayerData.UnlockedLevels;
            sceneTransition.EndSceneTransition("GameScene");
        }
        else
        {
            uIManager.OpenLevelsPanel();
        }
    }

    private void LoadlevelPanel()
    {
        int unlockedLevels = _persistentData.PlayerData.UnlockedLevels;
        
        for (int i = 0; i < levelSelectors.Count; i++)
        {
            if (unlockedLevels - 1 >= i)
            {
                levelSelectors[i].EnableLevel();

                int stars = 0;

                if (_persistentData.PlayerData.LevelsStars.ToList()[i] != null)
                    stars = _persistentData.PlayerData.LevelsStars.ToList()[i].Count();
                
                if (stars > 0)
                    levelSelectors[i].DisplayStar(stars);
            }
        }
    }

}
