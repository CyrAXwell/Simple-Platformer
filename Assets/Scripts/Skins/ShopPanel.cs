using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private ShopItemViewFactory shopItemViewFactory;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();
    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinsChecker _selectedSkinsChecker;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinsChecker selectedSkinsChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinsChecker = selectedSkinsChecker;
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();

        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = shopItemViewFactory.Get(item, itemsParent);

            spawnedItem.Click += OnItemViewClick;
            
            spawnedItem.Unselect();
            spawnedItem.UnHighlight();

            _openSkinsChecker.Visit(spawnedItem.Item);

            if (_openSkinsChecker.Isopened)
            {
                _selectedSkinsChecker.Visit(spawnedItem.Item);

                if (_selectedSkinsChecker.IsSelected)
                {
                    spawnedItem.Select();
                    spawnedItem.Highlight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }

                spawnedItem.Unlock();
            }
            else
            {
                spawnedItem.Lock();
            }

            _shopItems.Add(spawnedItem);
        }
    }

    public void Select(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
            item.Unselect();

        itemView.Select();
    }

    private void OnItemViewClick(ShopItemView itemView)
    {
        _audioManager.PlaySFX(_audioManager.SeelctButton);
        
        Highlight(itemView);
        ItemViewClicked?.Invoke(itemView);
    }

    private void Highlight(ShopItemView shopItemView)
    {
        foreach (var item in _shopItems)
            item.UnHighlight();

        shopItemView.Highlight();
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
