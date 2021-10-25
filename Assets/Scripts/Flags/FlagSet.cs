using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn;

namespace Flags {
    [CreateAssetMenu(fileName = "Flag Set", menuName = "Glittertools/FlagSet", order = 0)]
    public class FlagSet : SerializedScriptableObject {
        private List<Flag> yarnFlags;
        public List<DefaultVariable> defaultFlags;

        public void OnEnable() {
            yarnFlags = new List<Flag>();
            ResetToDefaults();
        }

        public void ResetToDefaults() {
            yarnFlags.Clear();

            // For each default variable that's been defined, parse the
            // string that the user typed in in Unity and store the
            // variable
            foreach (var variable in defaultFlags) {
                SetFlag("$" + variable.name,Flag.DefaultToValue(variable));
            }
        }
        
        public void SetFlag(string id, Yarn.Value v) {
            if (yarnFlags.Exists(a=>a.ID == id))
                yarnFlags.Find(a => a.ID == id).SetFlag(v);
            else
                yarnFlags.Add(new Flag(id, v));
        }
        public Yarn.Value GetFlag(string id) {
            if (yarnFlags.Exists(a=>a.ID.Equals(id)))
            {
                return yarnFlags.Find(a => a.ID.Equals(id)).ReadFlag();
            }
            
            throw new Exception("Flag " + id + " does not exist!");
        }

        public void Bind(string id, Flag.FlagDelegate del, bool bind = true) {
            if (yarnFlags.Exists(a=>a.ID.Equals(id)))
            {
                yarnFlags.Find(a => a.ID.Equals(id)).BindToDelegate(del, bind);
            } else
                throw new Exception("Flag " + id + " does not exist!");
        }
    }
}