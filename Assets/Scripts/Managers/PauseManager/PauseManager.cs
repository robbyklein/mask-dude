using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour {
  public static PauseManager Instance { get; private set; }
  [SerializeField] private InputHandlerSO input;

  private UIDocument uiDoc;
  private VisualElement rootEl;
  private VisualElement pauseEl;
  private VisualElement pauseLevelInner;
  private string activeClass = "pause--active";

  private void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }


  private void OnEnable() {
    uiDoc = GetComponent<UIDocument>();
    rootEl = uiDoc.rootVisualElement;
    pauseEl = rootEl.Q("pause");
    pauseLevelInner = rootEl.Q(className: "pause-header__level-inner");

    input.OnGamePause += HandlePause;
    input.OnMenuBack += HandleUnpause;

    _ = ShiftBackgroundPosition(pauseLevelInner);

  }

  private void OnDisable() {
    input.OnGamePause -= HandlePause;
    input.OnMenuBack -= HandleUnpause;
  }


  public void ShowPauseMenu() {
    pauseEl.AddToClassList(activeClass);
  }

  public void HidePauseMenu() {
    pauseEl.RemoveFromClassList(activeClass);

  }

  private async UniTaskVoid ShiftBackgroundPosition(VisualElement el) {
    int positionX = 0;
    while (true) {
      el.style.backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Left, positionX);
      positionX--;
      if (positionX <= -199)
        positionX = 0;
      await UniTask.Delay(TimeSpan.FromMilliseconds(33), cancellationToken: this.GetCancellationTokenOnDestroy(), ignoreTimeScale: true);
    }
  }

  private void HandlePause() {
    GameManager.Instance.Pause();
  }

  private void HandleUnpause() {
    GameManager.Instance.Unpause();
  }
}