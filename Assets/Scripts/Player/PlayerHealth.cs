using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxStartHealth = 1f;
    [SerializeField] private SceneTransition sceneTransition;

    private float _currentHealth;
    private AudioManager _audioManager;

    private void Awake() => _currentHealth = maxStartHealth;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, maxStartHealth);

        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {   
        _audioManager.PlaySFX(_audioManager.DeathSound);
        Time.timeScale = 0f;
        sceneTransition.EndSceneTransition("GameScene");
    }
}
