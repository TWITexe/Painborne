using UnityEngine;
using DG.Tweening;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    public AudioClip defaultMusic;

    [Header("Fade")]
    [SerializeField] private float fadeInDuration = 2f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float maxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
           PlayMusic(defaultMusic);
    }
    public void PlayMusic(AudioClip music)
    {
        if (music == null)
            return;

        // Если этот трек уже играет — ничего не делаем
        if (musicSource.clip == music && musicSource.isPlaying)
            return;

        musicSource.DOKill();

        musicSource.clip = music;
        musicSource.volume = 0f;
        musicSource.loop = true;

        musicSource.Play();

        musicSource
            .DOFade(maxVolume, fadeInDuration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
    }

    public void StopMusic()
    {
        if (!musicSource.isPlaying)
            return;

        //musicSource.DOKill();

        musicSource
            .DOFade(0f, fadeOutDuration)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                musicSource.Stop();
                musicSource.clip = null;
            });
    }
}