using UnityEngine;
using UnityEngine.Rendering;
using Cysharp.Threading.Tasks;
using System;

public class GrayscaleManager : MonoBehaviour {
    public static GrayscaleManager Instance { get; private set; }
    private Volume volume;
    public static Action<bool> GrayscaleChangeStart;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        volume = GetComponent<Volume>();
    }

    public async UniTaskVoid GrayscaleOn() {
        GrayscaleChangeStart?.Invoke(true);
        _ = AnimateVolumeWeight(0, 1, 300);
    }

    public async UniTaskVoid GrayscaleOff() {
        GrayscaleChangeStart?.Invoke(false);
        _ = AnimateVolumeWeight(1, 0, 300);
    }

    private async UniTask AnimateVolumeWeight(float start, float end, int duration) {
        float time = 0;

        while (time < duration) {
            float normalizedTime = time / duration;
            volume.weight = Mathf.Lerp(start, end, normalizedTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
            time += Time.unscaledDeltaTime * 1000;
        }

        volume.weight = end;
    }
}
