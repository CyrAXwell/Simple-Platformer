using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color lockColor;
    [SerializeField] private Color unlockColor;

    private void OnEnable() => button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => button.onClick.RemoveListener(OnButtonClick);

    public void UpdateText(int price) => text.text = price.ToString();

    public void Lock()
    {
        text.color = lockColor;
    }

    public void Unlock()
    {
        text.color = unlockColor;
    }

    private void OnButtonClick()
    {
        Click?.Invoke();
    }

}
