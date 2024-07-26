using System.Collections.Generic;
using UnityEngine;

public class GameSceneLevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;

    private int _currentLevel;

    public void Initialize(IDataProvider dataProvider, IPersistentData persistentPlayerData)
    {
        _currentLevel = LevelManager.currentLevel - 1;

        ClearScene();
        Initializelevel(dataProvider, persistentPlayerData);
    }

    private void Initializelevel(IDataProvider dataProvider, IPersistentData persistentPlayerData)
    {
        levels[_currentLevel].SetActive(true);
        levels[_currentLevel].GetComponent<StarsManager>().Initialize(persistentPlayerData);
        levels[_currentLevel].GetComponentInChildren<LevelFinish>().Initialize(dataProvider, persistentPlayerData);
    }

    private void ClearScene()
    {
        foreach (var level in levels)
        {
            if (level.activeInHierarchy)
                level.SetActive(false);
        }
    }
}
