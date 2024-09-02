using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RateGameExtern();
    
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private VolumeSettings settingsPanel;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject achievementsPanel;
    [SerializeField] private SkinDisplayMovement skinDisplayMovement;
    [SerializeField] private float fadeTime = 0.25f;
    [SerializeField] private float slideTime = 1f;
    [SerializeField] private GameObject rateButton;

    private CanvasGroup _mainPanelCanvasGroup;
    private RectTransform _levelsPanelRectTransform;
    private RectTransform _shopPanelRectTransform;
    private RectTransform _achievementsPanelRectTransform;
    private float _rightPanelPositionOffset = 1960f;
    private Image _settingsBackground;
    private RectTransform _settingsRectTransform;
    private float _topPanelPositionOffset = 1100f;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        _mainPanelCanvasGroup = mainPanel.GetComponent<CanvasGroup>();
        _mainPanelCanvasGroup.alpha = 0f;

        _levelsPanelRectTransform = levelsPanel.GetComponent<RectTransform>();
        _shopPanelRectTransform = shopPanel.GetComponent<RectTransform>();
        _achievementsPanelRectTransform = achievementsPanel.GetComponent<RectTransform>();

        _settingsBackground = settingsPanel.gameObject.GetComponent<Image>();
        _settingsRectTransform = settingsPanel.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();

        _settingsBackground.DOFade(0, 0); 
        _settingsRectTransform.DOAnchorPos(new Vector2(0f, _topPanelPositionOffset), 0f);

        settingsPanel.Initialize();
        settingsPanel.gameObject.SetActive(false);

        _mainPanelCanvasGroup.DOFade(1f, fadeTime);

        HidePanel(levelsPanel);
        HidePanel(shopPanel);
        HidePanel(achievementsPanel);
        HidePanel(skinDisplayMovement.gameObject);

    }

    private void HidePanel(GameObject panel) =>
        panel.SetActive(false);

    private void ShowPanel(GameObject panel) => 
        panel.SetActive(true);

    public void OpenMainPanel(GameObject hidePanel)
    {
        hidePanel.SetActive(false);
        _mainPanelCanvasGroup.DOFade(1f, fadeTime);
    }

    public void CloseMainPanel()
    {
        _mainPanelCanvasGroup.DOFade(0f, fadeTime);
    }

    public void OpenLevelsPanel()
    {
        ShowPanel(levelsPanel);
        PanelSlideIn();
        CloseMainPanel();
        _levelsPanelRectTransform.DOAnchorPos(Vector2.zero, slideTime).SetDelay(fadeTime);
        
    }

    public void CloseLevelsPanel()
    {
        PanelSlideOut();
        _levelsPanelRectTransform.DOAnchorPos(new Vector2(_rightPanelPositionOffset, 0f), slideTime).OnComplete(() => OpenMainPanel(levelsPanel));
    }

    public void OpenShopPanel()
    {
        ShowPanel(skinDisplayMovement.gameObject);
        ShowPanel(shopPanel);
        PanelSlideIn();
        CloseMainPanel();
        _shopPanelRectTransform.DOAnchorPos(Vector2.zero, slideTime).SetDelay(fadeTime);
    }

    public void CloseShopPanel()
    {
        HidePanel(skinDisplayMovement.gameObject);
        PanelSlideOut();
        _shopPanelRectTransform.DOAnchorPos(new Vector2(_rightPanelPositionOffset, 0f), slideTime).OnComplete(() => OpenMainPanel(shopPanel));
    }

    public void OpenAchievementsPanel()
    {
        ShowPanel(achievementsPanel);
        PanelSlideIn();
        CloseMainPanel();
        _achievementsPanelRectTransform.DOAnchorPos(Vector2.zero, slideTime).SetDelay(fadeTime);
    }

    public void CloseAchievementsPanel()
    {
        PanelSlideOut();
        _achievementsPanelRectTransform.DOAnchorPos(new Vector2(_rightPanelPositionOffset, 0f), slideTime).OnComplete(() => OpenMainPanel(achievementsPanel));
    }

    public void OpenSettingsPanel()
    {
        PanelSlideIn();
        ShowPanel(settingsPanel.gameObject);

        _settingsBackground.DOFade(0.5f, 1f).SetUpdate(true);
        _settingsRectTransform.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetDelay(0.3f).SetUpdate(true);  
    }

    public void CloseSettingsPanel()
    {
        PanelSlideOut();
        _settingsBackground.DOFade(0f, 0.2f).SetUpdate(true);
        _settingsRectTransform.DOAnchorPos(new Vector2(0f, _topPanelPositionOffset), 0.2f).SetUpdate(true).OnComplete(() => HidePanel(settingsPanel.gameObject));  
    }

    public void OnRateButton()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        RateGameExtern();
        #endif
    }

    public void HideRateButton()
    {
        rateButton.SetActive(false);
    }

    public void OnButtonClickSound() =>
        _audioManager.PlaySFX(_audioManager.ButtonClick);

    public void PanelSlideIn() =>
        _audioManager.PlaySFX(_audioManager.PanelSlideIn);

    public void PanelSlideOut() =>
        _audioManager.PlaySFX(_audioManager.PanelSlideOut);

    public void ClearPlayerData()
    {
        SceneManager.LoadScene("MainScene");
        PlayerPrefs.DeleteAll();
    }

}
