using UnityEngine;

public class CollideDustEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustEffect;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _audioManager.PlaySFX(_audioManager.TouchSound);

        dustEffect.Play();
    }
}
