using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Set", menuName = "Glittertools/Dialogue/SpriteSet", order = 0)]
public class CharacterSpriteSet : ScriptableObject {
    public string name;
    public List<Talksprite> sprites;
    
}

[Serializable]
public class Talksprite {
    public string id;
    public bool animated;
    public bool onceThrough;
    public Animator spriteAnimation;
    public Sprite staticSprite;
}