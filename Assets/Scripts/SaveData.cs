using System.Collections.Generic;
using Flags;
using UnityEngine;

namespace Assets.Scripts.Types
{
    [System.Serializable]
    public class SaveData {
        public List<Item> Inventory;
        public FlagSet Flags;
        public int Level;
        public Vector3 Position;
        public SaveData() {
            Inventory = new List<Item>();
            Flags = ScriptableObject.CreateInstance<FlagSet>();
            Level = -1;
            Position = new Vector3();
        }
    }
}
