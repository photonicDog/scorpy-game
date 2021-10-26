using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flags;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn;

public class InventoryManager : SerializedMonoBehaviour {
    
    public static InventoryManager Instance {
        get { return _instance; }
    }
    private static InventoryManager _instance;
    public List<Item> catalog;
    public List<Item> inventory;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }

        catalog.ForEach(a => inventory.Add(Instantiate(a)));
    }


    public List<Item> CurrentlyDisplayedInventory() {
        return inventory.Where(a => a.qty > 0).ToList();
    }
}

