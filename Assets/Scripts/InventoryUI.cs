using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    public GameObject ItemView;
    public RectTransform InventoryContent;
    
    public RectTransform scroll;
    private float anchor;

    private void Start() {
        ControlManager.Instance.controls.UI.Move.performed += Scroll;
        anchor = 0;
    }

    public void RegenerateInventory() {
        _inventoryManager = InventoryManager.Instance;
        anchor = 0;
        
        foreach (Transform o in InventoryContent.transform) {
            Destroy(o.gameObject);
        }
        
        foreach (Item item in _inventoryManager.CurrentlyDisplayedInventory())
        {
            var newItem = Instantiate(ItemView, InventoryContent.transform, false);
            UIItem uiItem = newItem.GetComponent<UIItem>();
            uiItem.SetTitle(item.Name);
            uiItem.SetDescription(item.Description);
            uiItem.SetSprite(item.ItemSprite);
            uiItem.SetQuantity(item.qty);
            newItem.SetActive(true);
        }
    }

    private void Scroll(InputAction.CallbackContext context) {
        anchor += 173 * context.ReadValue<Vector2>().y;
        scroll.anchoredPosition = new Vector2(0, anchor);
    }
    
    
}
