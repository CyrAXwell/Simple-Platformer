using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AchievementProgress : MonoBehaviour
{
    [SerializeField] TMP_Text counterText;
    [SerializeField] private Image slider;

    private int _value;
    private IPersistentData _persistentData;

    [Inject]
    private void Construct(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    public void Initialize(List<AchievementComponent> achievementComponents)
    {
        DisplayProgress(achievementComponents);
    }

    public void DisplayProgress(List<AchievementComponent> achievementComponents)
    {   
        _value = 0;
        
        foreach (AchievementComponent achievement in achievementComponents)
        {
            if (achievement.IsCompleted == true)
                _value ++;
        }

        float persentage = (float)_value / _persistentData.PlayerData.Achievements.Count();
        counterText.text = _value + "/" + _persistentData.PlayerData.Achievements.Count() + " (" + (persentage * 100).ToString("0.0") + "%)";

        slider.fillAmount = persentage;
    }
}
