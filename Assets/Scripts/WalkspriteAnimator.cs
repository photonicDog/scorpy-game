using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkspriteAnimator : MonoBehaviour {
    public Vector2 Facing;

    private Animator _animator;
    private Moveable _moveable;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int Walking = Animator.StringToHash("Walking");

    private void Start()
    {
        Facing = new Vector2(0, -1);
        _animator.SetFloat(X, 0);
        _animator.SetFloat(Y, -1);
    }

    // Start is called before the first frame update
    void OnEnable() {
        _moveable = GetComponent<Moveable>();
        _animator = GetComponentInChildren<Animator>();
        
        _moveable.OnWalk += Walk;
    }

    private void OnDisable() {
        _moveable.OnWalk -= Walk;
    }

    private void Walk(Vector2 direction) {
        if (direction.magnitude > 0.01) {
            _animator.SetFloat(X, direction.x);
            _animator.SetFloat(Y, direction.y);
            float magX = Mathf.Abs(direction.x);
            float magY = Mathf.Abs(direction.y);
            Facing = new Vector2(magX > magY ? 1 * Math.Sign(direction.x) : 0, magX < magY ? 1 * Math.Sign(direction.y) : 0);
            _animator.SetBool(Walking, true);
        }
        else {
            _animator.SetBool(Walking, false);
        }
    }
}
