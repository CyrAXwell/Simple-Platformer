using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private GameObject LevelsMenupanel;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLevelsMenu()
    {
        LevelsMenupanel.SetActive(true);
    }

}
