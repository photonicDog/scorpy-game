using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Moveable {

    public float StaminaMax;
    public float Stamina;
    public float StaminaRegenPerFrame;
    public float DelayOnFullBarUse;

    public float SprintMod;
    public float StaminaDrainPerFrame;

    public bool Sprint;

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

    private void Update()
    {
        if (Stamina > StaminaMax)
        {
            Stamina = StaminaMax;
        }
    }

    private void OnMove(InputAction.CallbackContext callbackContext) {
        Vector2 movement = callbackContext.ReadValue<Vector2>();
        if (Sprint)
        {
            movement *= SprintMod;
            Stamina -= StaminaDrainPerFrame;
        }
        OnMove(movement);
    }

    private void OnRelease(InputAction.CallbackContext callbackContext) {
        OnRelease();
    }
}
