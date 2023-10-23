using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public enum SceneOverlayMode {
    Fade,
    Circle,
}

public class SceneOverlayManager : MonoBehaviour {
    public static SceneOverlayManager Instance { get; private set; }
    [SerializeField] private UIDocument uiDoc;
    private VisualElement rootEl;
    private VisualElement sceneOverlayEl;
    private int totalTransitionTimeMs = 1300;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        FetchElements();
    }

    private void FetchElements() {
        rootEl = uiDoc.rootVisualElement;
        sceneOverlayEl = rootEl.Q("scene-overlay");
    }

    public void ChangeMode(SceneOverlayMode mode) {
        switch (mode) {
            case SceneOverlayMode.Fade:
                sceneOverlayEl.RemoveFromClassList("scene-overlay--circle");
                sceneOverlayEl.AddToClassList("scene-overlay--fade");
                break;
            case SceneOverlayMode.Circle:
                sceneOverlayEl.RemoveFromClassList("scene-overlay--fade");
                sceneOverlayEl.AddToClassList("scene-overlay--circle");
                break;
        }
    }

    public async UniTask Off() {
        sceneOverlayEl.AddToClassList("scene-overlay--off");

        await UniTask.Delay(
            TimeSpan.FromMilliseconds(totalTransitionTimeMs),
            ignoreTimeScale: false
        );
    }

    public async UniTask On() {
        sceneOverlayEl.RemoveFromClassList("scene-overlay--off");

        await UniTask.Delay(
            TimeSpan.FromMilliseconds(totalTransitionTimeMs),
            ignoreTimeScale: false
        );
    }
}
