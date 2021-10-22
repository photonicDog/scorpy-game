using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movable : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;

    [Header("Movement Parameters")] 
    [SerializeField] private float speed;
    
    private void FixedUpdate() {
        _rb.velocity = _frameVelocity;
    }

    protected void OnMove(InputAction.CallbackContext callbackContext) {
        _frameVelocity = callbackContext.ReadValue<Vector2>() * speed;
    }

    protected void OnRelease(InputAction.CallbackContext callbackContext) {
        _frameVelocity = Vector2.zero;
    }

    public void AssignRigidbody2D(Rigidbody2D rb) {
        _rb = rb;
    }
}
