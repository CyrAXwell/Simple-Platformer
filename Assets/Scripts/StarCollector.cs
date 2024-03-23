using UnityEngine;

public class StarCollector : MonoBehaviour
{
    [SerializeField] private GamePlayHUD gamePlayHUD;

    public void CollectStar()
    {
        gamePlayHUD.DisplayStar();
    }
}
