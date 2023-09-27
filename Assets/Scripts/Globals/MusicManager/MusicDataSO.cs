using System.Collections.Generic;
using UnityEngine;

public enum MusicName {
  Title
}

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicData")]
public class MusicDataSO : ScriptableObject {
  [System.Serializable]
  private struct MusicFile {
    public MusicName Name;
    public AudioClip MusicClip;
  }



  [SerializeField] private List<MusicFile> musicFiles;

  public AudioClip FetchMusicClip(MusicName name) {
    foreach (MusicFile file in musicFiles) {
      if (file.Name == name) {
        return file.MusicClip;
      }
    }

    return null;
  }

}