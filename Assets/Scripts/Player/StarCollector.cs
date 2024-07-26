using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour
{
    [SerializeField] private GamePlayHUD gamePlayHUD;
    
    private List<int> _stars = new List<int>();
    private string _starsList;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void CollectStar(int starIndex)
    {
        _audioManager.PlaySFX(_audioManager.StarCollected);

        _starsList += starIndex.ToString();
        _stars.Add(starIndex);
        
        gamePlayHUD.DisplayStar();
    }

    public int GetStarsNum()
    {
        return _stars.Count;
    }

    public string GetStarsList()
    {
        return _starsList;
    }
}
