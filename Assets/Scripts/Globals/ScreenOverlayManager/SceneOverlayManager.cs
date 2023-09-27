using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public enum SceneOverlayStyle {
    Bars,
    Fade,
}

public class SceneOverlayManager : MonoBehaviour {
    public static SceneOverlayManager Instance { get; private set; }
    [SerializeField] private SceneOverlayStyle style = SceneOverlayStyle.Fade;
    [SerializeField] private UIDocument uiDoc;
    private VisualElement rootEl;
    private VisualElement sceneOverlayEl;
    private int totalTransitionTimeMs = 1300;
    private int extraStallTimeMs = 500;

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
        ChangeStyle(style);
    }

    private void FetchElements() {
        rootEl = uiDoc.rootVisualElement;
        sceneOverlayEl = rootEl.Q("scene-overlay");
    }

    public void ChangeStyle(SceneOverlayStyle style) {
        switch (style) {
            case SceneOverlayStyle.Bars:
                sceneOverlayEl.RemoveFromClassList("scene-overlay--fade");
                sceneOverlayEl.AddToClassList("scene-overlay--bars");
                totalTransitionTimeMs = 1300;
                break;
            case SceneOverlayStyle.Fade:
                sceneOverlayEl.RemoveFromClassList("scene-overlay--bars");
                sceneOverlayEl.AddToClassList("scene-overlay--fade");
                totalTransitionTimeMs = 1000;
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
            TimeSpan.FromMilliseconds(totalTransitionTimeMs + extraStallTimeMs),
            ignoreTimeScale: false
        );
    }
}
