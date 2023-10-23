using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct MenuSetting {
    public string Name;
    public string Key;
    public List<string> Options;
}

public class SettingsMenuManager : MonoBehaviour {
    #region Dependencies
    [SerializeField] private SettingsManagerSO settings;
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private UIDocument uiDoc;
    [SerializeField] private List<MenuSetting> menuSettings;
    #endregion

    #region State
    private VisualElement rootEl, menuBox;
    int selectedSetting = 0;
    List<int> selectedOptions = new();
    #endregion

    #region Lifecycle
    private void OnEnable() {
        BuildMenuBase();
        BuildMenu();
        SubscribeToInput();
    }

    private void OnDisable() {
        UnsubscribeFromInput();
    }

    private void SubscribeToInput() {
        input.OnMenuUp += HandleMenuUp;
        input.OnMenuDown += HandleMenuDown;
        input.OnMenuLeft += HandleMenuLeft;
        input.OnMenuRight += HandleMenuRight;
        input.OnMenuBack += HandleMenuBack;
    }

    private void UnsubscribeFromInput() {
        input.OnMenuUp -= HandleMenuUp;
        input.OnMenuDown -= HandleMenuDown;
        input.OnMenuLeft -= HandleMenuLeft;
        input.OnMenuRight -= HandleMenuRight;
        input.OnMenuBack -= HandleMenuBack;
    }
    #endregion

    #region Building Menu
    private void BuildMenuBase() {
        // Set up base
        rootEl = uiDoc.rootVisualElement;
        rootEl.Add(MenuHelpers.BuildMenuBase());
        menuBox = rootEl.Q(className: MenuHelpers.boxClass);
    }

    private void BuildMenu() {
        VisualElement settingEl, optionsEl;
        string settingValue;

        // Add menu options
        foreach (MenuSetting setting in menuSettings) {
            settingEl = MenuHelpers.BuildSetting(setting.Name, setting.Key, setting.Name == menuSettings[0].Name);
            optionsEl = settingEl.Q(className: MenuHelpers.settingOptionsClass);
            settingValue = settings.GetSettingFromKey(setting.Key);

            if (setting.Key == "resolution") {
                AddResolutions(optionsEl);
            } else {
                AddOptions(setting.Options, settingValue, optionsEl);
            }

            menuBox.Add(settingEl);
        }
    }

    private void AddOptions(List<string> options, string settingValue, VisualElement optionsEl) {
        string option;
        bool isSelected;
        VisualElement optionEl;

        for (int i = 0; i < options.Count; i++) {
            option = options[i];
            isSelected = settingValue == option.ToLower();
            optionEl = MenuHelpers.BuildSettingOption(option, isSelected);
            if (isSelected) selectedOptions.Add(i);
            optionsEl.Add(optionEl);
        }
    }

    private void AddResolutions(VisualElement optionsEl) {
        string settingValue = settings.GetSettingFromKey("resolution");
        RefreshRate rr = Screen.currentResolution.refreshRateRatio;

        for (int i = 0; i < Screen.resolutions.Length; i++) {
            Resolution res = Screen.resolutions[i];
            if (!rr.Equals(res.refreshRateRatio)) continue;

            string text = ResolutionHelpers.createResolutionString(res);
            bool isSelected = settingValue == text.ToLower();
            if (isSelected) selectedOptions.Add(i);
            optionsEl.Add(MenuHelpers.BuildSettingOption(text, isSelected));
        }

    }
    #endregion

    #region Changing
    private void ChangeSelectedSetting(int newIndex) {
        List<VisualElement> settings = rootEl.Query(className: MenuHelpers.settingClass).ToList();
        settings[selectedSetting].RemoveFromClassList(MenuHelpers.settingSelectedClass);
        settings[newIndex].AddToClassList(MenuHelpers.settingSelectedClass);
        selectedSetting = newIndex;
    }

    private void ChangeSelectedOption(int newIndex) {
        // Get options list
        VisualElement settingEl = rootEl.Query(className: MenuHelpers.settingClass).AtIndex(selectedSetting);
        List<Label> optionEls = settingEl.Query<Label>(className: MenuHelpers.settingOptionClass).ToList();

        // Swap classes
        optionEls[selectedOptions[selectedSetting]].RemoveFromClassList(MenuHelpers.settingOptionSelectedClass);
        optionEls[newIndex].AddToClassList(MenuHelpers.settingOptionSelectedClass);

        // Change setting
        settings.ChangeSetting(menuSettings[selectedSetting].Key, optionEls[newIndex].text.ToLower());

        // Change selected index
        selectedOptions[selectedSetting] = newIndex;
    }

    private int GetTotalOptions() {
        List<VisualElement> settingEls = rootEl.Query(className: MenuHelpers.settingClass).ToList();
        List<VisualElement> optionEls = settingEls[selectedSetting].Query(className: MenuHelpers.settingOptionClass).ToList();
        return optionEls.Count - 1;
    }
    #endregion

    #region Input Handlers
    private void HandleMenuUp() {
        ChangeSelectedSetting(selectedSetting == 0 ? menuSettings.Count - 1 : selectedSetting - 1);
        SFXManager.Instance.PlaySFX(SFXName.Click);
    }
    private void HandleMenuDown() {
        ChangeSelectedSetting(selectedSetting < menuSettings.Count - 1 ? selectedSetting + 1 : 0);
        SFXManager.Instance.PlaySFX(SFXName.Click);
    }

    private void HandleMenuLeft() {
        int totalOptions = GetTotalOptions();
        int newIndex = selectedOptions[selectedSetting] == 0 ? totalOptions : selectedOptions[selectedSetting] - 1;
        ChangeSelectedOption(newIndex);
        SFXManager.Instance.PlaySFX(SFXName.Click);
    }

    private void HandleMenuRight() {
        int totalOptions = GetTotalOptions();
        int newIndex = selectedOptions[selectedSetting] == totalOptions ? 0 : selectedOptions[selectedSetting] + 1;
        ChangeSelectedOption(newIndex);
        SFXManager.Instance.PlaySFX(SFXName.Click);
    }
    private void HandleMenuBack() {
        GameManager.Instance.ChangeScene(GameScene.MainMenu);
    }
    #endregion
}
