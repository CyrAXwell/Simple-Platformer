using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private List<GameObject> stars;
    [SerializeField] private StarCollector starCollector;
    [SerializeField] private float duration;
    [SerializeField] private List<Button> buttons;

    private int _index = 0;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ShowLevelCompletePanel()
    {   
        SwitchButtonsState(false);

        text.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        text.text = "Уровень " + LevelManager.currentLevel + "\nпройден!";
        text.transform.DOScale( new Vector3(1f, 1f, 1f), 3f).SetEase(Ease.OutElastic).SetUpdate(true).SetDelay(0.7f).SetLink(gameObject);

        foreach (var star in stars)
            star.transform.localScale = new Vector3(1f, 1f, 1f);

        for (int i = 0; i < starCollector.GetStarsNum(); i++)
        {
            stars[i].transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 2f).SetEase(Ease.OutElastic).SetUpdate(true).SetDelay(1.5f + i*0.3f).OnStart(GetStar).SetLink(gameObject);
        }
    }

    private void GetStar()
    {
        _audioManager.PlaySFX(_audioManager.StarCount);
        Image image = stars[_index].GetComponent<Image>();
        Color tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        _index++;

        if (starCollector.GetStarsNum() == _index)
        {
            SwitchButtonsState(true);
        }
    }

    private void SwitchButtonsState(bool state)
    {
        foreach (Button button in buttons)
            button.interactable = state;
    }
}