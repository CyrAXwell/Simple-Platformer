using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePlayHUD : MonoBehaviour
{
    [SerializeField] private VolumeSettings pausePanelUI;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private LevelCompletePanel levelCompletePanel;
    [SerializeField] private GameObject levelLable;
    [SerializeField] private GameObject leveCompletePanelUI;
    [SerializeField] private GameObject levels;
    [SerializeField] private Button pauseButton;
    [SerializeField] private float fadeTime;
    [SerializeField] private float slideTime;
    [SerializeField] private List<GameObject> stars = new List<GameObject>();
    [SerializeField] private GameObject levelSkipPanelUI;
    [SerializeField] private GameObject levelSkipButton;
    
    private Image _pausePanelBackground;
    private RectTransform _menuRectTransform;
    private int _starIndex = 0;
    private LevelFinish _levelFinish;
    private AudioManager _audioManager;
    private bool _isLoadNewScene;
    private bool _isAdvStart;

    private void Awake()
    {
        _pausePanelBackground = pausePanelUI.GetComponent<Image>();
        _menuRectTransform = pausePanelUI.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();

        pausePanelUI.Initialize();

        InitializePanel(pausePanelUI.gameObject);
        InitializePanel(leveCompletePanelUI);
        InitializePanel(levelSkipPanelUI);
        
        _levelFinish = levels.transform.GetChild(LevelManager.currentLevel - 1).GetComponentInChildren<LevelFinish>();
        _isLoadNewScene = false;
        _isAdvStart = false;
    }   

    public void ShowSkipButton()
    {
        if (_levelFinish.IsCompletedLevel())
            levelSkipButton.SetActive(false);
    }

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        _levelFinish.LevelComplete += OpenLevelCompletePanel;
    }

    private void OnDisable()
    {
        _levelFinish.LevelComplete -= OpenLevelCompletePanel;
    }

    public void DisplayStar()
    {
        if (_starIndex < 3)
        {
            stars[_starIndex].SetActive(true);
            _starIndex ++;
        }
    }

    public void LoadMainScreen() =>
        SceneManager.LoadScene("MainScene");

    public void OpenPauseMenu()
    {
        if (Time.timeScale != 0f)
        {
            levelLable.SetActive(false);
            pausePanelUI.gameObject.SetActive(true);

            ShowPanel(_pausePanelBackground, _menuRectTransform);
        }
    }

    public void ClosePauseMenu()
    {
        _menuRectTransform.DOAnchorPos(new Vector2(0f, 1000f), 0.2f).SetUpdate(true).OnComplete(ResumeGame).SetLink(gameObject); 
        _pausePanelBackground.DOFade(0f, 0.2f).SetUpdate(true).OnComplete(DisablePausePanel);
    }

    private void DisablePausePanel()
    {
        levelLable.SetActive(true);
        pausePanelUI.gameObject.SetActive(false);
    }

    public void OpenLevelSkipMenu()
    {
        if (Time.timeScale != 0f)
        {
            levelLable.SetActive(false);
            levelSkipPanelUI.SetActive(true);

            Image background = levelSkipPanelUI.GetComponent<Image>();
            RectTransform rectTransform = levelSkipPanelUI.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();

            ShowPanel(background, rectTransform);

            pauseButton.interactable = false;
        }
    }

    public void CloseLevelSkipMenu()
    {
        Image background = levelSkipPanelUI.GetComponent<Image>();
        RectTransform rectTransform = levelSkipPanelUI.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();

        rectTransform.DOAnchorPos(new Vector2(0f, 1000f), 0.2f).SetUpdate(true).OnComplete(ResumeGame).SetLink(gameObject); 
        background.DOFade(0f, 0.2f).SetUpdate(true).OnComplete(DisableLevelSkipPanel);
        
        pauseButton.interactable = true;
    }

    private void DisableLevelSkipPanel()
    {
        levelLable.SetActive(true);
        levelSkipPanelUI.SetActive(false);
    }

    public void OpenLevelCompletePanel()
    {   
        if (!_levelFinish.IsLevelSkip)
        {
            leveCompletePanelUI.SetActive(true);
            levelSkipButton.SetActive(false);

            Image background = leveCompletePanelUI.GetComponent<Image>();
            RectTransform rectTransform = leveCompletePanelUI.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();

            ShowPanel(background, rectTransform);

            pauseButton.interactable = false;
            levelCompletePanel.ShowLevelCompletePanel();
        }
    }

    private void ShowPanel(Image background, RectTransform rectTransform)
    {
        PauseGame();

        background.DOFade(0.5f, fadeTime).SetUpdate(true);
        rectTransform.DOAnchorPos(new Vector2(0f, 5f), slideTime).SetDelay(0.3f).SetUpdate(true).SetLink(gameObject).SetLink(gameObject);   
    }

    private void InitializePanel(GameObject panel)
    {
        Image background = panel.GetComponent<Image>();
        background.DOFade(0f, 0f); 

        RectTransform rectTransform = panel.transform.GetChild(0).gameObject.GetComponentInChildren<RectTransform>();
        rectTransform.DOAnchorPos(new Vector2(0f, 1000f), 0f).SetLink(gameObject);

        panel.SetActive(false);
    }

    public void PauseGame() =>
        Time.timeScale = 0f;

    public void ResumeGame() =>
        Time.timeScale = 1f;

    public void ExitToMainMenu()
    {
        PauseGame();
        sceneTransition.EndSceneTransition("MainScene");
    }

    public void ResetLevel()
    {
        PauseGame();
        sceneTransition.EndSceneTransition("GameScene");
    }

    public void NextLevel()
    {
        if (!_isLoadNewScene)
        {
            _isLoadNewScene = true;
            PauseGame();
            if (LevelManager.currentLevel < LevelManager.maxLevels)
                LevelManager.currentLevel++;
            else if (LevelManager.currentLevel == LevelManager.maxLevels)
                LevelManager.currentLevel = 1;

            sceneTransition.EndSceneTransition("GameScene");
        }
    }

    public void SkipLevel()
    {
        // Включается реклама
        if (!_isAdvStart)
        {
            _isAdvStart = true;
            GameObject.FindGameObjectWithTag("ySDK").GetComponent<YandexSDK>().ShowRewardedAdv();  
        } 
    }

    public void SkipButtonState(bool state)
    {
        _isAdvStart = state;
    }

    public void SkipLevelReward()
    {
        // После просморта вызывается завершение уровня
        levelSkipPanelUI.SetActive(false);
        levelSkipButton.SetActive(false);
        _levelFinish.SkipLevel();
    }

    public void OnButtonClickSound() =>
        _audioManager.PlaySFX(_audioManager.ButtonClick);

}
