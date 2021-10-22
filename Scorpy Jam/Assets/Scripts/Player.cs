using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Controls controls;

    private PlayerMovement _movement;
    private PlayerInteraction _interaction;

    private void Awake() {
        _movement = GetComponent<PlayerMovement>();
        _interaction = GetComponent<PlayerInteraction>();
        
        _movement.AssignRigidbody2D(GetComponent<Rigidbody2D>());
    }
    
    
}
