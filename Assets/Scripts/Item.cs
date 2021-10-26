using System;
using System.Collections;
using System.Linq;
using Flags;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn;

[InlineEditor()]
[CreateAssetMenu(fileName = "Item", menuName = "Glittertools/Inventory/Item", order = 0)]
public class Item : SerializedScriptableObject {
    
    [VerticalGroup("ItemSet")]
    [HorizontalGroup("ItemSet/Name", LabelWidth = 20)]
    public string ID;
    [HorizontalGroup("ItemSet/Name", LabelWidth = 40)] [SuffixLabel("$qty", Overlay = true)]
    public string Name;
    [HorizontalGroup("ItemSet/Item", 60)] [HideLabel] [PreviewField(60)] public Sprite ItemSprite;
    [HorizontalGroup("ItemSet/Item", LabelWidth = 60)] [TextArea] public string Description;
    [HideInInspector] public int qty;
    [ValueDropdown("GetAllValidFlags")] public string associatedFlagID;
    private bool None => qty <= 0;

    public virtual void Obtain() {
        qty++;
        if (associatedFlagID != "") {
            FlagManager.Instance.SetFlag("$"+associatedFlagID, new Value(true));
        }
    }

    public virtual void ObtainMany(int i) {
        for (int j = 0; j < i; j++) {
            Obtain();
        }
    }

    public virtual void Lose() {
        if (!None) qty--;
        if (None) {
            if (associatedFlagID!="") FlagManager.Instance.SetFlag("$"+associatedFlagID, new Value(false));
        }
    }

    public virtual void LoseMany(int i) {
        for (int j = 0; j < i; j++) {
            Lose();
        }
    }

    public void LoseAll() {
        while (!None) {
            Lose();
        }
    }

    private IEnumerable GetAllValidFlags() {
        FlagSet fs = UnityEditor.AssetDatabase.LoadAssetAtPath<FlagSet>("Assets/Data/Flag Set.asset");
        return fs.defaultFlags.Select(a => a.name);
    }
}