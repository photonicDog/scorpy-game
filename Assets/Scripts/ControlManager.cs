using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    
    public static ControlManager Instance {
        get { return _instance; }
    }
    private static ControlManager _instance;

    public Controls controls;
    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }

        controls = new Controls();
    }

    public void SwitchControlSchema(ControlSchema schema) {
        controls.Gameplay.Disable();
        controls.UI.Disable();
        controls.Dialogue.Disable();

        switch (schema) {
            case ControlSchema.GAMEPLAY:
                controls.Gameplay.Enable();
                break;
            case ControlSchema.UI:
                controls.UI.Enable();
                break;
            case ControlSchema.DIALOGUE:
                controls.Dialogue.Enable();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
        }
    }

    public void SetControlGameplay() => SwitchControlSchema(ControlSchema.GAMEPLAY);
    public void SetControlUI() => SwitchControlSchema(ControlSchema.UI);
    public void SetControlDialogue() => SwitchControlSchema(ControlSchema.DIALOGUE);
}

public enum ControlSchema {
    GAMEPLAY,
    UI,
    DIALOGUE
}
