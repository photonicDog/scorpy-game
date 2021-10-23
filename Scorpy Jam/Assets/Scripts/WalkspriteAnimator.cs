using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkspriteAnimator : MonoBehaviour {

    private Animator _animator;
    private Moveable _moveable;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int Walking = Animator.StringToHash("Walking");

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
            _animator.SetBool(Walking, true);
        }
        else {
            _animator.SetBool(Walking, false);
        }
    }
}
