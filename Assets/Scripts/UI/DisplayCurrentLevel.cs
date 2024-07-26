using TMPro;
using UnityEngine;
using DG.Tweening;

public class DisplayCurrentLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text lable;

    private RectTransform _lableRectTransform => lable.gameObject.GetComponent<RectTransform>();

    private void Awake()
    {
        lable.alpha = 0f;
        lable.text = "Уровень " + (LevelManager.currentLevel);
        lable.DOFade(1f, 1f);
        _lableRectTransform.DOAnchorPos(Vector2.zero, 1f).SetLink(gameObject);
        lable.DOFade(0f, 1f).SetDelay(1.1f);
    }

}
