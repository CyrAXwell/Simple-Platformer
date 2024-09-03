using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TrailFactory trailFactory;
    [SerializeField] private OrbFactory orbFactory;
    [SerializeField] private GamePlayHUD gamePlayHUD;

    private IPersistentData _persistentPlayerData;

    [Inject]
    private void Construct( IPersistentData persistentPlayerData)
    {
        _persistentPlayerData = persistentPlayerData;
    }

    private void Awake()
    {
        gamePlayHUD.transform.parent.gameObject.SetActive(true);
        GetSkins();
        gamePlayHUD.ShowSkipButton();
    }

    private void GetSkins()
    {
        GameObject trail = trailFactory.Get(_persistentPlayerData.PlayerData.SelectedTrailSkin, player);
        GameObject orb = orbFactory.Get(_persistentPlayerData.PlayerData.SelectedOrbSkin, player);
    }
}