using UnityEngine;

class ResolutionHelpers {
  static public string createResolutionString(Resolution res) {
    return $"{res.width}x{res.height}/{res.refreshRateRatio}hz";
  }

  static public Resolution FindResolutionByString(string resString) {
    for (int i = 0; i < Screen.resolutions.Length; i++) {
      if (createResolutionString(Screen.resolutions[i]) == resString) {
        return Screen.resolutions[i];
      }
    }

    return Screen.currentResolution;
  }
}