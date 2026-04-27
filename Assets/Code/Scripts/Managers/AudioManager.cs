using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip ziplineShoot;
    public AudioClip ziplineHit;
    public AudioClip crumble;
    public AudioClip death;
    public AudioClip victory;
    public AudioClip click;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
