using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public struct PauseMenuOption {
  public string Name;
  public string Key;
}

public class PauseManager : MonoBehaviour {
  #region Dependencies
  [SerializeField] private InputHandlerSO input;
  #endregion

  #region State
  [SerializeField] private List<PauseMenuOption> menuOptions;
  public static PauseManager Instance { get; private set; }

  private int selectedSection = 0;
  private VisualElement selectedSectionEl;

  private int selectedItem = 0;
  private VisualElement selectedItemEl;
  #endregion

  #region UI Elements
  private UIDocument uiDoc;
  private VisualElement rootEl;
  private VisualElement pauseEl;
  private VisualElement levelInnerEl;
  private VisualElement navEl;
  #endregion

  #region Classes
  private string cls = "pause";
  private string activeCls = "pause--active";
  private string levelInnerCls = "pause-header__level-inner";
  private string sectionCls = "pause-section";
  private string sectionActiveCls = "pause-section--active";
  private string navCls = "pause-section--nav";
  private string navItemCls = "nav-item";
  private string navItemSelectedCls = "nav-item--selected";
  private string navItemPressedCls = "nav-item--pressed";
  #endregion

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
    // Grab els
    uiDoc = GetComponent<UIDocument>();
    rootEl = uiDoc.rootVisualElement;
    pauseEl = rootEl.Q(cls);
    levelInnerEl = rootEl.Q(className: levelInnerCls);
    navEl = rootEl.Q(className: navCls);

    // Subscribe to events
    input.OnGamePause += HandlePause;
    input.OnMenuBack += HandleUnpause;
    input.OnMenuDown += HandleDown;
    input.OnMenuUp += HandleUp;
    input.OnMenuSelect += HandleSelect;

    // Build the menus
    BuildNav();

    // Enable header level animation
    _ = ShiftBackgroundPosition(levelInnerEl);
  }

  private void OnDisable() {
    // Unsubscribe from events
    input.OnGamePause -= HandlePause;
    input.OnMenuBack -= HandleUnpause;
    input.OnMenuDown -= HandleDown;
    input.OnMenuUp -= HandleUp;
    input.OnMenuSelect -= HandleSelect;
  }
  #endregion

  private void BuildNav() {
    // Add nav items
    foreach (PauseMenuOption option in menuOptions) {
      VisualElement menuButton = PauseManagerHelpers.BuildMenuButton(option.Name);
      navEl.Add(menuButton);
    }

    // Add animations to buttons
    _ = ShiftBackgroundPositionForDots();
  }

  private void SelectOption() {
    if (selectedItemEl != null) {
      selectedItemEl.RemoveFromClassList(navItemSelectedCls);
    }

    selectedItemEl = rootEl.Query(className: navItemCls).AtIndex(selectedItem);
    if (selectedItemEl != null) {
      selectedItemEl.AddToClassList(navItemSelectedCls);
      _ = CycleActiveLetter(selectedItemEl);
    }
  }

  private async UniTaskVoid SelectPage() {
    VisualElement selected = rootEl.Q(className: sectionActiveCls);
    if (selected != null) {
      selected.RemoveFromClassList(sectionActiveCls);
      await UniTask.Delay(TimeSpan.FromMilliseconds(300), ignoreTimeScale: true);
    }

    rootEl.Query(className: sectionCls).AtIndex(selectedSection).AddToClassList(sectionActiveCls);
  }

  private void ResetPauseUI() {
    pauseEl.RemoveFromClassList(activeCls);
    rootEl.Q(className: sectionActiveCls).RemoveFromClassList(sectionActiveCls);
    rootEl.Q(className: navItemSelectedCls).RemoveFromClassList(navItemSelectedCls);
    selectedItem = 0;
    selectedSection = 0;
  }

  #region Animations
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

  private async UniTaskVoid ShiftBackgroundPositionForDots() {
    var elements = uiDoc.rootVisualElement.Query(className: "pause-button__dots").ToList();
    int positionX = 0;

    while (true) {
      positionX--;

      if (positionX <= -5) {
        positionX = 0;
      }

      foreach (var el in elements) {
        el.style.backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Left, positionX);
      }

      await UniTask.Delay(TimeSpan.FromMilliseconds(33), cancellationToken: this.GetCancellationTokenOnDestroy(), ignoreTimeScale: true);
    }
  }

  private async UniTaskVoid CycleActiveLetter(VisualElement selectedItemEl) {
    var letters = selectedItemEl.Query<Label>(className: "pause-button__letter").ToList();

    while (selectedItemEl == this.selectedItemEl) {
      foreach (var letter in letters) {
        if (selectedItemEl != this.selectedItemEl) break;

        letter.AddToClassList("pause-button__letter--active");
        await UniTask.Delay(TimeSpan.FromMilliseconds(200), cancellationToken: this.GetCancellationTokenOnDestroy(), ignoreTimeScale: true);
        letter.RemoveFromClassList("pause-button__letter--active");
      }

      if (selectedItemEl != this.selectedItemEl) break;
      await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: this.GetCancellationTokenOnDestroy(), ignoreTimeScale: true);
    }
  }
  #endregion

  #region On/off and menu switches
  public void ShowPauseMenu() {
    SelectOption();
    pauseEl.AddToClassList(activeCls);
    SelectPage();
  }

  public void HidePauseMenu() {
    ResetPauseUI();
  }
  #endregion

  #region Event handlers
  private void HandlePause() {
    GameManager.Instance.Pause();
  }

  private void HandleUnpause() {
    GameManager.Instance.Unpause();
  }

  private void HandleUp() {
    if (selectedItem == 0) {
      selectedItem = menuOptions.Count - 1;
    } else {
      selectedItem -= 1;
    }

    SelectOption();
  }

  private void HandleDown() {
    if (selectedItem == menuOptions.Count - 1) {
      selectedItem = 0;
    } else {
      selectedItem += 1;
    }

    SelectOption();
  }

  private async void HandleSelect() {
    _ = HandleSelectAsync();
  }

  private async UniTask HandleSelectAsync() {
    await PressButton();
    if (selectedItemEl != null) selectedItemEl.RemoveFromClassList(navItemPressedCls);
  }

  private async UniTask PressButton() {
    if (selectedItemEl != null) selectedItemEl.AddToClassList(navItemPressedCls);
    await UniTask.Delay(TimeSpan.FromMilliseconds(100), ignoreTimeScale: true);
  }
  #endregion
}