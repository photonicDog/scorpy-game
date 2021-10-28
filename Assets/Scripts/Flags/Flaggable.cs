using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Yarn;

namespace Flags {
    public class Flaggable : SerializedMonoBehaviour {
        public Dictionary<string, Flag.FlagDelegate> FlagEvents;

        private void OnEnable() {
            foreach (KeyValuePair<string, Flag.FlagDelegate> e in FlagEvents) {
                FlagManager.Instance.Bind("$"+e.Key, e.Value);
                FlagManager.Instance.SetFlag(e.Key, FlagManager.Instance.GetFlag(e.Key));
            }
        }
        
        private void OnDisable() {
            foreach (KeyValuePair<string, Flag.FlagDelegate> e in FlagEvents) {
                FlagManager.Instance.Bind("$"+e.Key, e.Value, false);
            }
        }
    }
}