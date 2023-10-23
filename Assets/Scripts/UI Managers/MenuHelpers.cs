using UnityEngine;
using UnityEngine.UIElements;

class MenuHelpers {
  static public string rootClass = "settings";
  static public string boxClass = "settings__box";
  static public string settingClass = "settings-row";
  static public string settingSelectedClass = "settings-row--selected";
  static public string settingLabelClass = "settings-row__label";
  static public string settingOptionsClass = "settings-row__options";
  static public string settingOptionClass = "settings-row__option";
  static public string settingOptionSelectedClass = "settings-row__option--selected";

  static public VisualElement BuildMenuBase() {
    VisualElement menu = new VisualElement();
    menu.AddToClassList(rootClass);

    VisualElement menuBox = new VisualElement();
    menuBox.AddToClassList(boxClass);

    menu.Add(menuBox);

    return menu;
  }

  static public VisualElement BuildSetting(string label, string name = "", bool selected = false) {
    // Create root
    VisualElement setting = new VisualElement();
    setting.AddToClassList(settingClass);
    if (name != "") setting.name = name;

    // Add label
    Label settingLabel = new Label(label.ToUpper());
    settingLabel.AddToClassList(settingLabelClass);
    setting.Add(settingLabel);

    // Add options el
    VisualElement options = new VisualElement();
    options.AddToClassList(settingOptionsClass);
    setting.Add(options);

    if (selected) setting.AddToClassList(settingSelectedClass);

    return setting;
  }

  static public Label BuildSettingOption(string text, bool selected = false) {
    Label option = new Label(text.ToUpper());
    option.AddToClassList(settingOptionClass);
    if (selected) option.AddToClassList(settingOptionSelectedClass);
    return option;
  }
}