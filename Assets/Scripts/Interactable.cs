using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public abstract class Interactable : MonoBehaviour {
    protected DialogueRunner _dr;

    private void Awake() {
        _dr = DialogueManager.Instance.Runner;
    }

    public abstract void Interact();
}
