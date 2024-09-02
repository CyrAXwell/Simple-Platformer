using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdvExtern();

    [DllImport("__Internal")]
    private static extern void ShowRewardedAdvExtern();
    
    [SerializeField] private float advInterval;
    [SerializeField] private AudioManager audioManager;

    private static float _timer;
    private static bool _isRewarded;

    public static YandexSDK instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _timer = advInterval;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_timer > 0f)
            _timer -= Time.deltaTime;
        else if (_timer < 0f)
            _timer = 0f;
    }

    public void ShowAdv()
    {
        if (_timer == 0)
        {
            _timer = advInterval;
            #if UNITY_WEBGL && !UNITY_EDITOR
            ShowFullscreenAdvExtern();
            PausedSound();
            #endif
        }
    }

    public void ShowRewardedAdv()
    {
        _isRewarded = false;
        #if UNITY_WEBGL && !UNITY_EDITOR
        ShowRewardedAdvExtern();
        #endif
    }

    public void OnGetReward()
    {
        _isRewarded = true;
        GameObject.Find("HUD Layer").GetComponent<GamePlayHUD>().SkipLevelReward(); 
    }

    public void OnCloseAdv()
    {
        if (_isRewarded)
        {
            GameObject.Find("HUD Layer").GetComponent<GamePlayHUD>().NextLevel(); 
            GameObject.Find("HUD Layer").GetComponent<GamePlayHUD>().SkipButtonState(true);
        }
        else
        {
            GameObject.Find("HUD Layer").GetComponent<GamePlayHUD>().SkipButtonState(false);
        }
        ResumeSound();       
    }

    public void ResumeSound()
    {
        audioManager.ResumeSound();
    }

    public void PausedSound()
    {
        audioManager.PauseSound();
    }

    public void HideRateButton()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().HideRateButton();
    }
}
