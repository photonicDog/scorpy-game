using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flags;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn;

public class InventoryManager : SerializedMonoBehaviour {
    
    public static InventoryManager Instance {
        get { return _instance; }
    }

    public InventoryUI inventoryUI;
    
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
        HideInventory(new InputAction.CallbackContext());
        catalog.ForEach(a => inventory.Add(Instantiate(a)));
    }

    private void Start() {
        ControlManager.Instance.controls.Gameplay.Menu.performed += DisplayInventory;
        ControlManager.Instance.controls.UI.Exit.performed += HideInventory;
    }

    private void OnDisable() {
        ControlManager.Instance.controls.Gameplay.Menu.performed -= DisplayInventory;
        ControlManager.Instance.controls.UI.Exit.performed -= HideInventory;
    }
    
    public List<Item> CurrentlyDisplayedInventory() {
        return inventory.Where(a => a.qty > 0).ToList();
    }

    public void DisplayInventory(InputAction.CallbackContext context) {
        ControlManager.Instance.controls.Gameplay.Disable();
        ControlManager.Instance.controls.UI.Enable();
        inventoryUI.gameObject.SetActive(true);
        inventoryUI.RegenerateInventory();
    }

    private void HideInventory(InputAction.CallbackContext context) {
        ControlManager.Instance.controls.UI.Disable();
        ControlManager.Instance.controls.Gameplay.Enable();
        inventoryUI.gameObject.SetActive(false);
    }
}

