using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Text Title;
    public Text Description;
    public Image Sprite;

    public void SetTitle(string title)
    {
        Title.text = title;
    }

    public void SetDescription(string desc)
    {
        Description.text = desc;
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite.sprite = sprite;
    }
}
