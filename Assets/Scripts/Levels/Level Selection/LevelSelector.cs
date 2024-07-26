using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private CanvasGroup levelIcon;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private List<Image> stars;

    private int _levelIndex;

    public int LevelIndex => _levelIndex;

    public void Initialize(int index)
    {
        _levelIndex = index;
        levelText.text = _levelIndex.ToString();
    }

    public void EnableLevel()
    {
        selectButton.interactable = true;
        levelIcon.alpha = 1f;
    }

    public void DisplayStar(int starsNumber)
    {
        for (int i = 0; i < starsNumber; i++)
        {
            Color newColor = stars[i].color;
            newColor.a = 1f;
            stars[i].color = newColor;
        }
    }
}
