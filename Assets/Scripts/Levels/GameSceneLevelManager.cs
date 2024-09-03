using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneLevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;

    private int _currentLevel;

    [Inject]
    private void Construct(IDataProvider dataProvider, IPersistentData persistentPlayerData, Wallet wallet)
    {
        _currentLevel = LevelManager.currentLevel - 1;

        ClearScene();
        Initializelevel(dataProvider, persistentPlayerData, wallet);
    }

    private void Initializelevel(IDataProvider dataProvider, IPersistentData persistentPlayerData, Wallet wallet)
    {
        levels[_currentLevel].SetActive(true);
        levels[_currentLevel].GetComponent<StarsManager>().Initialize(persistentPlayerData);
        levels[_currentLevel].GetComponentInChildren<LevelFinish>().Initialize(dataProvider, persistentPlayerData, wallet);
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
