using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadiusTriggerComponent : MonoBehaviour {

    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnExit;
    [SerializeField] private UnityEvent OnStay;
    private void OnTriggerEnter2D(Collider2D other) {
        OnEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) {
        OnExit?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other) {
        OnStay?.Invoke();
    }
}
