using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] private List<Interactable> _interactablesInRadius;

    private BoxCollider2D collideableArea;

    private void Awake() {
        _interactablesInRadius = new List<Interactable>();
        collideableArea = GetComponent<BoxCollider2D>();
    }

    private void OnEnable() {
        ControlManager.Instance.controls.Gameplay.Interact.started += OnInteract;
    }

    private void OnDisable() {
        ControlManager.Instance.controls.Gameplay.Interact.started -= OnInteract;
    }
    
    public void OnInteract(InputAction.CallbackContext obj) {
        if (_interactablesInRadius.Count > 0) {
            Debug.Log("Interact!");
            _interactablesInRadius[0].Interact();
        }
    }
    
    public void SetCurrentInteractable(Interactable interactable) {
        _interactablesInRadius.Add(interactable);
    }
    
    public void RemoveInteractable(Interactable interactable) {
        _interactablesInRadius.Remove(interactable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collideable collide = collision.gameObject.GetComponent<Collideable>();
        if (collide != null && collision.bounds.Intersects(collideableArea.bounds))
        {
            collide.Activate();
        }
    }
}