using Unity.VisualScripting;
using UnityEngine;

public class collectableStar : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<StarCollector>().CollectStar();
            gameObject.SetActive(false);
        }
    }
}
