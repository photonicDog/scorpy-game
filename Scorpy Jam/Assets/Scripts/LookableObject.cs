using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookableObject : Interactable {
    public string NodeName;


    public override void Interact() {
        _dr.StartDialogue(NodeName);
    }
}
