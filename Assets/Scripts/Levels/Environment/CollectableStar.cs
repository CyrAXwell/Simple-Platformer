using UnityEngine;

public class CollectableStar : MonoBehaviour
{
    [SerializeField, Range(0, 2)] private int starIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<StarCollector>().CollectStar(starIndex);
            gameObject.SetActive(false);
        }
    }
}
