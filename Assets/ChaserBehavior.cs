using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserBehavior : Moveable {
    private NavMeshAgent _agent;
    private WalkspriteAnimator _walksprite;
    
    private Transform _player;

    private Vector2 _move;
    // Start is called before the first frame update
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updatePosition = true;
        _agent.updateUpAxis = false;
        
        AssignRigidbody2D(GetComponent<Rigidbody2D>());

        _walksprite = GetComponent<WalkspriteAnimator>();

        _player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    protected void Update() {
        Chase();
    }

    protected override void FixedUpdate() {
    }

    void Chase() {
        _agent.SetDestination(_player.position);
        OnWalk?.Invoke(_agent.velocity);

    }

    private Vector2 ConstrainTo8Dir(Vector2 velocity) {
        Vector2 res = velocity.normalized;

        float x;
        float y;

        if (res.x > 0.33f) x = 1f;
        else if (res.x > -0.33f) x = 0f;
        else x = -1f;
        
        if (res.y > 0.33f) y = 1f;
        else if (res.y > -0.33f) y = 0f;
        else y = -1f;
        
        return new Vector2(x, y).normalized;
    }
}
