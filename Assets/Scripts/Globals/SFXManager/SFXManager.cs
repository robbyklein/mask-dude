using UnityEngine;
using Cysharp.Threading.Tasks;

public class SFXManager : MonoBehaviour {
    public static SFXManager Instance { get; private set; }

    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] SFXDataSO sfxData;
    [SerializeField] SettingsManagerSO settings;

    #region Lifecycle
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        settings.OnSFXVolumeChange += ChangeVolume;
    }

    private void OnDisable() {
        settings.OnSFXVolumeChange -= ChangeVolume;
    }

    private void Start() {
        ChangeVolume(settings.sfxVolume);
    }
    #endregion

    public void ChangeVolume(string volumeString) {
        float volume;

        if (float.TryParse(volumeString, out volume)) {
            volume = volume / 10;
        } else {
            volume = 1;
        }

        sfxPlayer.volume = volume;
    }

    public void PlaySFX(SFXName name) {
        AudioClip clip = sfxData.FetchSFXClip(name);
        sfxPlayer.PlayOneShot(clip);
    }
}
