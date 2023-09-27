using System.Collections.Generic;
using UnityEngine;

public enum SFXName {
    Click
}

[CreateAssetMenu(fileName = "SFXData", menuName = "ScriptableObjects/SFXData")]
public class SFXDataSO : ScriptableObject {
    [System.Serializable]
    private struct SFXFile {
        public SFXName Name;
        public AudioClip SFXClip;
    }
    [SerializeField] private List<SFXFile> sfxFiles;

    public AudioClip FetchSFXClip(SFXName name) {
        foreach (SFXFile file in sfxFiles) {
            if (file.Name == name) {
                return file.SFXClip;
            }
        }

        return null;
    }
}