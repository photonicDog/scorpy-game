using System;
using System.Collections;
using System.Collections.Generic;
using Flags;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class DialogueInventoryHandler : MonoBehaviour
{
    private DialogueManager _dialogueManager;

    void Awake() {
        _dialogueManager = GetComponent<DialogueManager>();
        DialogueRunner _dr = _dialogueManager.Runner;

        _dr.AddCommandHandler(
            "give",
            GiveItem);
        
        _dr.AddCommandHandler(
            "take",
            TakeItem);
        
        _dr.AddCommandHandler(
            "icM",
            ICM);
        
        _dr.AddCommandHandler(
            "icL",
            ICL);
        
        _dr.AddCommandHandler(
            "icE",
            ICE);
    }
    
    void GiveItem(string[] parameters) {
        InventoryManager.Instance.inventory.Find(a => a.ID == parameters[0]).ObtainMany(Convert.ToInt32(parameters[1]));
    }
    
    void TakeItem(string[] parameters) {
        if (Convert.ToInt32(parameters[1]) == 0) {
            InventoryManager.Instance.inventory.Find(a => a.ID == parameters[0]).LoseAll();
        } else
            InventoryManager.Instance.inventory.Find(a => a.ID == parameters[0]).LoseMany(Convert.ToInt32(parameters[1]));
    }
    
    void CompareItem(string itemID, int quantity, Func<int, int, bool> compare, string flag) {
        if (compare.Invoke(InventoryManager.Instance.inventory.Find(a => a.ID == itemID).qty, quantity)) {
            FlagManager.Instance.SetFlag(flag, new Value(true));
        }
    }

    void ICM(string[] parameters) {
        CompareItem(parameters[0], Int32.Parse(parameters[1]), (i, j) => i>j, parameters[2]);
    }
    
    void ICL(string[] parameters) {
        CompareItem(parameters[0], Int32.Parse(parameters[1]), (i, j) => i<j, parameters[2]);
    }
    
    void ICE(string[] parameters) {
        CompareItem(parameters[0], Int32.Parse(parameters[1]), (i, j) => i==j, parameters[2]);
    }

}
