using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Wallet
{
    [DllImport("__Internal")]
    private static extern void AddCoinsExtern(int value);
    
    public event Action<int> CoinsChanged;

    private IPersistentData _persistentData;

    public Wallet(IPersistentData persistentData) => _persistentData = persistentData;

    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        
        _persistentData.PlayerData.Money += coins;
        _persistentData.PlayerData.MaxMoney += coins;

#if UNITY_WEBGL && !UNITY_EDITOR
        GameObject yandexSDK = GameObject.FindGameObjectWithTag("ySDK");
        if (yandexSDK != null)
            AddCoinsExtern(_persistentData.PlayerData.MaxMoney);  
#endif
        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }

    public int GetCurrentCoins() => _persistentData.PlayerData.Money;

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return _persistentData.PlayerData.Money >= coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money -= coins;

        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }
}
