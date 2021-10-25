using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Flags {
    [CreateAssetMenu(fileName = "Flag Set", menuName = "Glittertools/FlagSet", order = 0)]
    public class FlagSet : SerializedScriptableObject {
        public List<Flag<int>> intFlags;
        public List<Flag<string>> stringFlags;
        public List<Flag<bool>> boolFlags;
        
        public void SetFlag(string id, int v) {
            intFlags.Find(a => a.ID == id).SetFlag(v);
        }
        
        public void SetFlag(string id, string v) {
            stringFlags.Find(a => a.ID == id).SetFlag(v);
        }

        public void SetFlag(string id, bool v) {
            boolFlags.Find(a => a.ID == id).SetFlag(v);
        }
        
        public object GetFlag(string id) {
            if (intFlags.Exists(a=>a.ID == id)) 
                return intFlags.Find(a => a.ID == id).ReadFlag();
            if (stringFlags.Exists(a => a.ID == id)) 
                return stringFlags.Find(a => a.ID == id).ReadFlag();
            if (boolFlags.Exists(a => a.ID == id)) 
                return boolFlags.Find(a => a.ID == id).ReadFlag();

            throw new Exception("Flag " + id + " does not exist!");
        }
    }
}