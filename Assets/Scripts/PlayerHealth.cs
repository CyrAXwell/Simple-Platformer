using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxStartHealth = 1f;

    private float currentHealt;

    private void Awake() => currentHealt = maxStartHealth;
    

    public void TakeDamage(float damage)
    {
        currentHealt = Mathf.Clamp(currentHealt - damage, 0f, maxStartHealth);

        if (currentHealt > 0)
        {
            
        }
        else
        {
            Die();
        }

    }

    private void Die()
    {
        SceneManager.LoadScene(1);
    }
}
