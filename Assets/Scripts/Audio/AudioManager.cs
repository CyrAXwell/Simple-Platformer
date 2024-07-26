using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    [Header("Music")]
    public AudioClip GamePlayBackground;
    public AudioClip MainBackground;

    [Header("UI")]
    public AudioClip ButtonClick;
    public AudioClip PanelSlideIn;
    public AudioClip PanelSlideOut;
    public AudioClip BuyButton;
    public AudioClip SeelctButton;
    public AudioClip SceneTransition;
    public AudioClip GetReward;

    [Header("Player")]
    public AudioClip DeathSound;
    public AudioClip TouchSound;
    public AudioClip StarCollected;

    [Header("Level")]
    public AudioClip LevelComplete;
    public AudioClip StarCount;
    public AudioClip GetAchievement;

    public static AudioManager instance;
    private static float _sfxVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.clip = MainBackground;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip, 1f);
    }

    public void PauseSound()
    {
        sFXSource.mute = true;
        musicSource.mute = true;
    }

    public void ResumeSound()
    {
        sFXSource.mute = false;
        musicSource.mute = false;
    }
}
