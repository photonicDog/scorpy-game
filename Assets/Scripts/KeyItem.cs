using UnityEngine;

[CreateAssetMenu(fileName = "Key Item", menuName = "Glittertools/Inventory/Key Item", order = 1)]
public class KeyItem : Item {
    public delegate void KeyItemDelegate();
    public KeyItemDelegate Delegate;
    
    public override void Obtain() {
        base.Obtain();
        Delegate?.Invoke();
    }
}