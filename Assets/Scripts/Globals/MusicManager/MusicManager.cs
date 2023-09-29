using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] AudioSource musicPlayer;
    [SerializeField] MusicDataSO musicData;
    [SerializeField] SettingsManagerSO settings;

    #region Lifecycle
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        settings.OnMusicVolumeChange += ChangeVolume;
    }

    private void OnDisable()
    {
        settings.OnMusicVolumeChange -= ChangeVolume;
    }

    private void Start()
    {
        ChangeVolume(settings.musicVolume);
    }
    #endregion

    public void ChangeVolume(string volumeString)
    {
        float volume;

        if (float.TryParse(volumeString, out volume)) {
            volume = volume / 10 / 2;
        } else {
            volume = 1;
        }

        musicPlayer.volume = volume;
    }

    public void PlaySong(MusicName name)
    {
        AudioClip clip = musicData.FetchMusicClip(name);
        if (clip == musicPlayer.clip) return;

        musicPlayer.clip = clip;
        musicPlayer.Play();
    }

    public async UniTask FadeOut(float duration)
    {
        float startVolume = musicPlayer.volume;
        float elapsed = 0f;

        while (musicPlayer.volume > 0) {
            elapsed += Time.deltaTime;
            musicPlayer.volume = Mathf.Clamp01(startVolume - (elapsed / duration) * startVolume);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        musicPlayer.volume = 0;
        musicPlayer.Stop();
    }
}
