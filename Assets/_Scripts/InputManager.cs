using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonPersistent<InputManager>
{
    [SerializeField]
    PlatformerManager _platformerManager;

    [SerializeField]
    GameScenesManager _gameScenesManager;

    [SerializeField]
    GameObject _keyboardControls1;

    [SerializeField]
    GameObject _keyboardControls2;

    [SerializeField]
    GameObject _touchScreenGeneral;

    [SerializeField]
    GameObject touchScreenControls0;
    [SerializeField]
    GameObject touchScreenControls1;
    [SerializeField]
    GameObject touchScreenControls2;

    public override void Awake()
    {
        _platformerManager = PlatformerManager.Instance;
        _gameScenesManager = GameScenesManager.Instance;
        base.Awake();
        if(_platformerManager.isThisAPhone)
        {
            TurnOnTouchScreenControls(_gameScenesManager.sceneNumber);
        }
        else
        {
            TurnOnKeyboardControls(_gameScenesManager.sceneNumber);
        }

    }

    private void OnEnable()
    {
        GameScenesManager.OnSceneChange += TurnOnKeyboardControls;
        GameScenesManager.OnSceneChange += TurnOnTouchScreenControls;
    }

    private void OnDisable()
    {
        GameScenesManager.OnSceneChange -= TurnOnKeyboardControls;
        GameScenesManager.OnSceneChange -= TurnOnTouchScreenControls;
    }

    private void TurnOnKeyboardControls(int sceneNumber)
    {
        if(!_platformerManager.isThisAPhone)
        {
            Debug.Log("Keyboard controls are on!");
            if (sceneNumber == 0)
            {
                Debug.Log("Enabling Keyboard for UI");
            }
            else if (sceneNumber == 1)
            {
                Debug.Log("Enabling Keyboard for PongMatch");
                _keyboardControls1.SetActive(true);
                _keyboardControls2.SetActive(true);

            }
            else if (sceneNumber == 2)
            {
                Debug.Log("Enabling Keyboard for UI");
            }
        }
    }

    private void TurnOnTouchScreenControls(int sceneNumber)
    {
        if (_platformerManager.isThisAPhone)
        {
            Debug.Log("Touchscreen controls are on!");
            if (sceneNumber == 0)
            {
                Debug.Log("Enabling Touchscreen controls for UI");
                _touchScreenGeneral.SetActive(true);
            }
            else if (sceneNumber == 1)
            {
                Debug.Log("Enabling Touchscreen controls for PongMatch");
                touchScreenControls0.SetActive(true);
                touchScreenControls1.SetActive(true);
                touchScreenControls2.SetActive(true);

            }
            else if (sceneNumber == 2)
            {
                Debug.Log("Enabling Touchscreen controls for UI");

            }
        }
    }
}
