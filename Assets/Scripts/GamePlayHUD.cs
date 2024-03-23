using System.Collections.Generic;
using UnityEngine;


public class GamePlayHUD : MonoBehaviour
{

    [SerializeField] private List<GameObject> stars = new List<GameObject>();

    private int _index = 0;

    public void DisplayStar()
    {
        if (_index < 2)
        {
            stars[_index].SetActive(true);
            _index ++;
        }
    }
}
