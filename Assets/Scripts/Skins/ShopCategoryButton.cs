using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color unselectColor;
    [SerializeField] private TMP_Text tMPText;

    private void OnEnable() => button.onClick.AddListener(OnClick);

    private void OnDisable() => button.onClick.RemoveListener(OnClick);

    public void Select() 
    {
        image.color = selectColor;
        tMPText.color = selectColor;
    }

    public void Unselect()
    {
        image.color = unselectColor;
        tMPText.color = unselectColor;
    } 

    private void OnClick() => Click?.Invoke();

}
