using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Image frame;
    [SerializeField] private Sprite standartBackground;
    [SerializeField] private Sprite highlightBackground;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Image contentImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private IntValueView priceView;
    [SerializeField] private Image selectionText;

    private Image _backgroundImage;

    public ShopItem Item { get; private set; }
    public bool IsLock { get; private set; }
    public int Price => Item.Price;
    public GameObject Model => Item.Model;

    public void Initialize(ShopItem item)
    {
        _backgroundImage = frame;
        _backgroundImage.sprite = standartBackground;

        Item = item;

        contentImage.sprite = item.Sprite;

        priceView.Show(Price);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click?.Invoke(this);
    } 

    public void Lock()
    {
        IsLock = true;
        lockImage.gameObject.SetActive(IsLock);
        priceView.Show(Price);
    }

    public void Unlock()
    {
        IsLock = false;
        lockImage.gameObject.SetActive(IsLock);
        priceView.Hide();
    }

    public void Select()
    {
        selectionText.gameObject.SetActive(true);
    }

    public void Unselect()
    {
        selectionText.gameObject.SetActive(false);
    } 
    
    public void Highlight()
    {
        _backgroundImage.sprite = highlightBackground;
        _backgroundImage.color = highlightColor;
    } 

    public void UnHighlight()
    {
        _backgroundImage.sprite = standartBackground;
        _backgroundImage.color = Color.white;
    }
}   
