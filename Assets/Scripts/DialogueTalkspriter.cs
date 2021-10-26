using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueTalkspriter : MonoBehaviour {
    
    public Image talksprite;
    public Animator animator;

    public List<CharacterSpriteSet> sprites;

    private DialogueRunner _dr;

    // Start is called before the first frame update
    void Start() {
        _dr = GetComponent<DialogueRunner>();

        _dr.AddCommandHandler(
            "c",
            UpdateCharacterSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateCharacterSprite(string[] parameters) {
        Debug.Log("Sprite updated!");
        Talksprite t = sprites.Find(a => a.name == parameters[0])
                      .sprites.Find(a => a.id == parameters[1]);
        if (t.animated) {
            
        }
        else {
            talksprite.sprite = t.staticSprite;
        }
    }
}
