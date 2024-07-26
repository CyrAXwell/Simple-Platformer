using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
    [SerializeField] private string description;
    [SerializeField] private GameObject icon;
    [SerializeField] private int maxValue;
    [SerializeField] private int reward;
    [field: SerializeField] public AchievementTypes Type { get; private set; }

    public string Description => description;
    
    public GameObject Icon => icon;

    public int MaxValue => maxValue;

    public int Reward => reward;
}
