using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text value;

    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
        UpdateValue(_wallet.GetCurrentCoins());

        _wallet.CoinsChanged += UpdateValue;
    }

    private void OnDestroy() => _wallet.CoinsChanged -= UpdateValue;

    private void UpdateValue(int value)
    {
        this.value.text = value.ToString();
    }
     
}
