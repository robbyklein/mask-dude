using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsManager", menuName = "ScriptableObjects/SettingsManager")]
public class SettingsManagerSO : ScriptableObject {
  public string resolution;
  public string display;
  public string musicVolume;
  public string sfxVolume;

  private void OnEnable() {
    LoadSettings();
  }

  private void LoadSettings() {
    // Load music
    display = PlayerPrefs.GetString("display", "fullscreen");
    resolution = PlayerPrefs.GetString("resolution", ResolutionHelpers.createResolutionString(Screen.currentResolution));
    musicVolume = PlayerPrefs.GetString("musicVolume", "10");
    sfxVolume = PlayerPrefs.GetString("sfxVolume", "10");
  }

  public string GetSettingFromKey(string key) {
    switch (key) {
      case "display":
        return display;
      case "resolution":
        return resolution;
      case "musicVolume":
        return musicVolume;
      case "sfxVolume":
        return sfxVolume;
      default:
        return null; // or throw new ArgumentException($"Invalid key: {key}");
    }
  }

  public void ChangeSetting(string key, string value) {
    // Save it
    PlayerPrefs.SetString(key, value);

    // Handle it
    switch (key) {
      case "display":
        HandleDisplayChange(value);
        break;
      case "resolution":
        HandleResolutionChange(value);
        break;
      case "musicVolume":
        break;
      case "sfxVolume":
        break;
      default:
        throw new InvalidOperationException($"Invalid settings key: {key}");
    }
  }

  private void HandleDisplayChange(string displayType) {
    switch (displayType) {
      case "fullscreen":
        Screen.fullScreen = true;
        break;
      case "windowed":
        Screen.fullScreen = false;
        break;
      default:
        throw new InvalidOperationException($"Invalid display type: {displayType}");
    }
  }

  private void HandleResolutionChange(string resolutionString) {
    Resolution newResolution = ResolutionHelpers.FindResolutionByString(resolutionString);
    Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreenMode, newResolution.refreshRateRatio);
  }

}