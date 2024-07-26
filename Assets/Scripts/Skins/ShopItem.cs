using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameObject Model { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField, Range(0, 30)] public int Price { get; private set; }

}
