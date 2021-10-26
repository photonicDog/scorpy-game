using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance {
        get { return _instance; }
    }
    private static DialogueManager _instance;
    
    public DialogueRunner Runner;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this);
        }
        else {
            _instance = this;
        }
    }
}
