using TMPro;
using UnityEngine;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private Transform iconTransform;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private RectTransform display;

    public RectTransform Display => display;

    public void Initialize(Achievement achievement)
    {
        descriptionTMP.text = achievement.Description;
        Instantiate(achievement.Icon, iconTransform);
        counterText.text = achievement.MaxValue.ToString() + "/" + achievement.MaxValue.ToString();
        rewardText.text = achievement.Reward.ToString();
    }
}
