using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> stars = new List<SpriteRenderer>();

    private IPersistentData _persistentPlayerData;

    public void Initialize(IPersistentData persistentPlayerData)
    {
        _persistentPlayerData = persistentPlayerData;

        UpdateStars();
    }

    public void UpdateStars()
    {
        string starsList = _persistentPlayerData.PlayerData.LevelsStars.ToList()[LevelManager.currentLevel-1];
        
        if (starsList != null)
        {
            foreach( char starIndex in starsList)
            {
                Color newColor = stars[int.Parse(starIndex.ToString())].color;
                newColor.a = 0.05f;
                stars[int.Parse(starIndex.ToString())].color = newColor;
            }
        }
    }
}
