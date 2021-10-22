using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTriggerComponent : MonoBehaviour {

    [SerializeField] private InteractableEvent OnEnter;
    [SerializeField] private InteractableEvent OnExit;
    [SerializeField] private InteractableEvent OnStay;
    private void OnTriggerEnter2D(Collider2D other) {
        if (IsInteractable(other.gameObject, out Interactable interactable)) OnEnter?.Invoke(interactable);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (IsInteractable(other.gameObject, out Interactable interactable)) OnExit?.Invoke(interactable);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (IsInteractable(other.gameObject, out Interactable interactable)) OnStay?.Invoke(interactable);
    }

    private bool IsInteractable(GameObject go, out Interactable interactable) {
        return go.TryGetComponent(out interactable);
    }
}
