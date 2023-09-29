using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private UIDocument uiDoc;
    private VisualElement uiRoot;
    private int selectedIndex = 0;
    private string optionClass = "main-menu__option";
    private string optionSelectedClass = "main-menu__option--selected";

    private void OnEnable()
    {
        uiRoot = uiDoc.rootVisualElement;

        input.OnMenuLeft += HandleSwitch;
        input.OnMenuRight += HandleSwitch;
        input.OnMenuUp += HandleSwitch;
        input.OnMenuDown += HandleSwitch;
        input.OnMenuBack += HandleQuit;
        input.OnMenuSelect += HandleSelect;
    }

    private void OnDisable()
    {
        input.OnMenuLeft -= HandleSwitch;
        input.OnMenuRight -= HandleSwitch;
        input.OnMenuUp -= HandleSwitch;
        input.OnMenuDown -= HandleSwitch;
        input.OnMenuBack -= HandleQuit;
        input.OnMenuSelect -= HandleSelect;
    }

    private void HandleQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    private void HandleSwitch()
    {
        List<VisualElement> options = uiRoot.Query(className: optionClass).ToList();
        options[selectedIndex].RemoveFromClassList(optionSelectedClass);

        if (selectedIndex == 0) {
            selectedIndex = 1;
        } else {
            selectedIndex = 0;
        }

        options[selectedIndex].AddToClassList(optionSelectedClass);

    }

    private void HandleSelect()
    {
        if (selectedIndex == 0) {
            GameManager.Instance.ChangeScene(GameScene.Game);
        } else {
            GameManager.Instance.ChangeScene(GameScene.Settings);
        }
    }
}
