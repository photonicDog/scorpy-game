using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Controls controls;

    private PlayerMovement _movement;
    private PlayerInteraction _interaction;
    private Rigidbody2D _rb;
    private SpriteRenderer _spRender;

    public bool Hidden;

    private void Awake() {
        _movement = GetComponent<PlayerMovement>();
        _interaction = GetComponent<PlayerInteraction>();
        _spRender = gameObject.GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();


        _movement.AssignRigidbody2D(_rb);
    }
    
    public void Hide()
    {
        _rb.Sleep();
        _spRender.enabled = false;
        DisableMovement();
        Hidden = true;
    }

    public void Unhide()
    {
        _rb.WakeUp();
        _spRender.enabled = true;
        EnableMovement();
        Hidden = false;
    }

    public void DisableMovement()
    {
        _movement.enabled = false;
    }

    public void EnableMovement()
    {
        _movement.enabled = true;
    }
}
