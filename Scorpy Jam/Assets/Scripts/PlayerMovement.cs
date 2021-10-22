using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;

    [Header("Movement Parameters")] 
    [SerializeField] private float speed;
    
    
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

    private void FixedUpdate() {
        _rb.velocity = _frameVelocity;
    }

    private void OnMove(InputAction.CallbackContext callbackContext) {
        _frameVelocity = callbackContext.ReadValue<Vector2>() * speed;
    }

    private void OnRelease(InputAction.CallbackContext callbackContext) {
        _frameVelocity = Vector2.zero;
    }

    public void AssignRigidbody2D(Rigidbody2D rb) {
        _rb = rb;
    }
}
