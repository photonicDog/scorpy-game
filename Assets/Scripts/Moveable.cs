using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moveable : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;

    [Header("Movement Parameters")] 
    [SerializeField] private float speed;
    
    public delegate void MoveDelegate(Vector2 direction);
    public MoveDelegate OnWalk;
    
    private void FixedUpdate() {
        _rb.velocity = _frameVelocity;
    }

    protected void OnMove(InputAction.CallbackContext callbackContext) {
        Vector2 input = callbackContext.ReadValue<Vector2>();
        _frameVelocity = input * speed;
        OnWalk?.Invoke(input);
    }

    protected void OnRelease(InputAction.CallbackContext callbackContext) {
        _frameVelocity = Vector2.zero;
        OnWalk?.Invoke(Vector2.zero);
    }

    public void AssignRigidbody2D(Rigidbody2D rb) {
        _rb = rb;
    }
}