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
    public bool CanMove = true;

    private InputAction.CallbackContext lastMoveInput;

    private void FixedUpdate() {
        _rb.velocity = _frameVelocity;
    }

    protected void OnMove(InputAction.CallbackContext callbackContext) {
        if (!lastMoveInput.Equals(callbackContext)) lastMoveInput = callbackContext;
        if (!CanMove) return;
        Vector2 input = callbackContext.ReadValue<Vector2>();
        _frameVelocity = input * speed;
        OnWalk?.Invoke(input);
    }

    protected void OnRelease(InputAction.CallbackContext callbackContext) {
        Stop();
    }

    public void Stop()
    {
        _frameVelocity = Vector2.zero;
        StopAllCoroutines();
        OnWalk?.Invoke(Vector2.zero);
    }

    public void PauseMovement() {
        CanMove = false;
        Stop();
        Debug.Log("Movement paused.");
        StartCoroutine(WaitForMove(lastMoveInput));
    }
    
    private IEnumerator WaitForMove(InputAction.CallbackContext callbackContext) {
        Debug.Log("Halting movement...");
        yield return new WaitUntil(() => CanMove);
        Debug.Log("Resuming movement.");
        OnMove(callbackContext);
    }
    
    public void AssignRigidbody2D(Rigidbody2D rb) {
        _rb = rb;
    }
}
