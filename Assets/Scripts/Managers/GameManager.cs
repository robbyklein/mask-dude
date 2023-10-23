
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public enum GameScene {
    MainMenu,
    Settings,
    Splash,
    Game,
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] MusicManager musicManager;
    [SerializeField] InputHandlerSO input;
    private GameScene currentScene = GameScene.MainMenu;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Pause() {
        Time.timeScale = 0;
        PauseManager.Instance.ShowPauseMenu();
        _ = GrayscaleManager.Instance.GrayscaleOn();
        input.ChangeInputMap(InputHandlerSO.InputMap.Menu);
    }

    public void Unpause() {
        PauseManager.Instance.HidePauseMenu();
        _ = GrayscaleManager.Instance.GrayscaleOff();
        input.ChangeInputMap(InputHandlerSO.InputMap.Game);
        Time.timeScale = 1;
    }


    public async UniTask ChangeScene(GameScene newGameScene) {
        input.DisableInput();

        switch (newGameScene) {
            case GameScene.Splash:
                await SceneManager.LoadSceneAsync("Splash");
                await SceneOverlayManager.Instance.Off();
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                await ChangeScene(GameScene.MainMenu);
                break;
            case GameScene.MainMenu:
                await SceneOverlayManager.Instance.On();
                SceneOverlayManager.Instance.ChangeMode(SceneOverlayMode.Circle);
                await SceneManager.LoadSceneAsync("MainMenu");
                musicManager.PlaySong(MusicName.Title);
                input.ChangeInputMap(InputHandlerSO.InputMap.Menu);
                await SceneOverlayManager.Instance.Off();
                break;
            case GameScene.Settings:
                await SceneOverlayManager.Instance.On();
                await SceneManager.LoadSceneAsync("Settings");
                input.ChangeInputMap(InputHandlerSO.InputMap.Menu);
                await SceneOverlayManager.Instance.Off();
                break;
            case GameScene.Game:
                await SceneOverlayManager.Instance.On();
                await SceneManager.LoadSceneAsync("Game");
                await SceneOverlayManager.Instance.Off();
                input.ChangeInputMap(InputHandlerSO.InputMap.Game);
                break;
        }

        currentScene = newGameScene;
    }
}
