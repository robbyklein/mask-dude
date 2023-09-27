using UnityEngine;

public class SceneFowarder : MonoBehaviour {
    [SerializeField] GameScene scene;

    void Start() {
        GameManager.Instance.ChangeScene(scene);
    }
}
