using System.Collections;
using System.Collections.Generic;
using Flags;
using UnityEngine;

public class LookableObject : Interactable, IFlagChangeable {
    public string NodeName;

    public override void Interact() {
        _dr.StartDialogue(NodeName);
        OnInteract?.Invoke();
    }
    
    public void Alter(Yarn.Value alter) {
        NodeName = alter.AsString;
    }
}
