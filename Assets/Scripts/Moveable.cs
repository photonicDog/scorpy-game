using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moveable : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _frameVelocity;

    [Header("Movement Parameters")] 
    [SerializeField] public float speed;

    public delegate void MoveDelegate(Vector2 direction);
    public MoveDelegate OnWalk;
    public bool CanMove = true;

    private Vector2 lastMoveInput;

    protected virtual void FixedUpdate() {
        _rb.velocity = _frameVelocity;
    }

    protected void OnMove(Vector2 vec) {
        if (!lastMoveInput.Equals(vec)) lastMoveInput = vec;
        if (!CanMove) return;
        _frameVelocity = vec * speed;
        OnWalk?.Invoke(vec);
    }

    protected void OnRelease() {
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
    
    private IEnumerator WaitForMove(Vector2 vec) {
        Debug.Log("Halting movement...");
        yield return new WaitUntil(() => CanMove);
        Debug.Log("Resuming movement.");
        OnMove(vec);
    }
    
    public void AssignRigidbody2D(Rigidbody2D rb) {
        _rb = rb;
    }
}
