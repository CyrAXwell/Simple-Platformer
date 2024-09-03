using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AchievementComponent : MonoBehaviour
{
    public event Action GetRewardClick;

    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private Transform iconTransform;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Image slider;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private Button rewardButton;
    [SerializeField] private GameObject completedIcon;

    private int _currentValue;
    private int _maxValue;
    private bool _isCompleted;
    private bool _isRewarded;
    private AchievementTypes _type;
    private Achievement _achievement;
    private IPersistentData _persistentData;
    private Wallet _wallet;
    private int _id;
    private IDataProvider _dataProvider;

    public AchievementTypes Type => _type;
    public bool IsCompleted => _isCompleted;
    public int Id => _id;
    
    [Inject]
    private void Construct(IPersistentData persistentData, IDataProvider dataProvider, Wallet wallet)
    {
        _persistentData = persistentData;
        _wallet = wallet;
        _dataProvider = dataProvider;
    }

    public void Initialize(Achievement achievement, int id)
    {
        _achievement = achievement;
        
        _id = id;
        

        descriptionTMP.text = _achievement.Description;

        Instantiate(_achievement.Icon, iconTransform);

        SetValue();

        _maxValue = _achievement.MaxValue;
        _isRewarded = _persistentData.PlayerData.GetAchievementState(_id);
        _isCompleted = _currentValue >= _maxValue;

        if (!_isRewarded)
        {
            completedIcon.SetActive(false);
            rewardButton.gameObject.SetActive(true);
            rewardText.text = _achievement.Reward.ToString();

            if (!_isCompleted)
            {
                rewardButton.interactable = false;
            }
            else
            {
                rewardButton.interactable = true;
                rewardButton.GetComponent<CanvasGroup>().alpha = 1;
            }  
        }
        else
        {
            completedIcon.SetActive(true);
            rewardButton.gameObject.SetActive(false);
        }

        counterText.text = _currentValue.ToString() + "/" + _maxValue.ToString();

        slider.fillAmount = (float)_currentValue/_maxValue;
    }

    public void UpdateValue()
    {
        descriptionTMP.text = _achievement.Description;
        SetValue();

        _isRewarded = _persistentData.PlayerData.GetAchievementState(_id);
        _isCompleted = _currentValue >= _maxValue;

        if (!_isRewarded)
        {
            completedIcon.SetActive(false);
            rewardButton.gameObject.SetActive(true);
            rewardText.text = _achievement.Reward.ToString();

            if (!_isCompleted)
            {
                rewardButton.interactable = false;
            }
            else
            {
                rewardButton.interactable = true;
                rewardButton.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            completedIcon.SetActive(true);
            rewardButton.gameObject.SetActive(false);
        }

        counterText.text = _currentValue.ToString() + "/" + _maxValue.ToString();

        slider.fillAmount = (float)_currentValue/_maxValue;

    }

    public bool CheckValue()
    {
        if (_isCompleted == false)
        {
            SetValue();

            _isCompleted = _currentValue >= _maxValue;

            if (_isCompleted == true)
                return true;
        }
        return false;
    }

    private void SetValue()
    {
        _type = _achievement.Type;
        
        switch (_type)
        {
            case AchievementTypes.Levels:
                _currentValue = _persistentData.PlayerData.CompletedLevels;
                break;
            case AchievementTypes.Stars:
                _currentValue = _persistentData.PlayerData.GetStarsAmount();
                break;
            case AchievementTypes.FullLevels:
                _currentValue = _persistentData.PlayerData.GetFullLevelCompletedAmount();
                break;
            case AchievementTypes.OrbSkins:
                _currentValue = _persistentData.PlayerData.OpenOrbSkins.ToList().Count();
                break;
            case AchievementTypes.TrailSkins:
                _currentValue = _persistentData.PlayerData.OpenTrailSkins.ToList().Count();
                break;
        }
    }

    public void GetReward()
    {
        GetRewardClick?.Invoke();
        
        _isRewarded = true;
        completedIcon.SetActive(true);
        rewardButton.gameObject.SetActive(false);

        _wallet.AddCoins(_achievement.Reward);
        _persistentData.PlayerData.CompleteAchievement(_id);

        _dataProvider.Save();
    }
}
