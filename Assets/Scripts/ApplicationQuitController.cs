using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationQuitController : MonoBehaviour {

    private static ApplicationQuitController _instance;
    public static ApplicationQuitController Instance {
        get { return _instance; }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

    private void Start() {
        ControlManager.Instance.controls.Gameplay.Quit.performed += HoldQuit;
    }

    private void HoldQuit(InputAction.CallbackContext context) {
        Application.Quit();
    }
    public void Quit() {
        Application.Quit();
    }
}
