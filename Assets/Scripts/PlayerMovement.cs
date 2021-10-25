using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Moveable {

    private void OnEnable() {
        ControlManager.Instance.controls.Gameplay.Move.performed += OnMove;
        ControlManager.Instance.controls.Gameplay.Move.canceled += OnRelease;
    }

    private void OnDisable() {
        ControlManager.Instance.controls.Gameplay.Move.performed -= OnMove;
        ControlManager.Instance.controls.Gameplay.Move.canceled -= OnRelease;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Put this in UIStateManager
        ControlManager.Instance.SwitchControlSchema(ControlSchema.GAMEPLAY);
    }
}
