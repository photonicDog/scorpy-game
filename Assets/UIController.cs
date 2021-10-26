using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    public GameObject ItemView;
    public RectTransform InventoryContent;

    // Start is called before the first frame update
    void Start()
    {
        _inventoryManager = InventoryManager.Instance;
        List<GameObject> items = new List<GameObject>();

        foreach (Item item in _inventoryManager.inventory)
        {
            var newItem = Instantiate(ItemView, InventoryContent.transform, false);
            UIItem uiItem = newItem.GetComponent<UIItem>();
            uiItem.SetTitle(item.Name);
            uiItem.SetDescription(item.Description);
            uiItem.SetSprite(item.ItemSprite);
            items.Add(newItem);
            newItem.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
