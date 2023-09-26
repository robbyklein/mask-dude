
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public enum GameScene {
    MainMenu,
    Game
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private GameScene currentScene = GameScene.MainMenu;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public async UniTask ChangeScene(GameScene newGameScene) {
        switch (newGameScene) {
            case GameScene.MainMenu:
                await SceneManager.LoadSceneAsync("MainMenu");
                await SceneOverlayManager.Instance.Off();
                break;
            case GameScene.Game:
                await SceneOverlayManager.Instance.On();
                await SceneManager.LoadSceneAsync("Game");
                await SceneOverlayManager.Instance.Off();
                break;
        }

        currentScene = newGameScene;
    }
}
