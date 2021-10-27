using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Quantity;
    public Image Sprite;
    
    public void SetTitle(string title)
    {
        Title.text = title;
    }

    public void SetDescription(string desc)
    {
        Description.text = desc;
    }

    public void SetQuantity(int qty) {
        Quantity.text = qty.ToString();
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite.sprite = sprite;
    }
}
