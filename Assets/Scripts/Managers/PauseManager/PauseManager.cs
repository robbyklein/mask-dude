using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour {
  public static PauseManager Instance { get; private set; }
  [SerializeField] private InputHandlerSO input;

  private UIDocument uiDoc;
  private VisualElement rootEl;
  private VisualElement pauseEl;
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

    input.OnGamePause += HandlePause;
    input.OnMenuBack += HandleUnpause;
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

  private void HandlePause() {
    GameManager.Instance.Pause();
  }

  private void HandleUnpause() {
    GameManager.Instance.Unpause();
  }
}