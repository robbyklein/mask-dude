
using UnityEngine;

public class GlobalLoader : MonoBehaviour {
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private SettingsManagerSO settings;

    void Start() {
        GameManager.Instance.ChangeScene(GameScene.MainMenu);
    }
}
