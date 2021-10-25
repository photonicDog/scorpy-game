using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Yarn;

namespace Flags {
    public class FlagEvent : SerializedMonoBehaviour, IFlagChangeable {
        public Dictionary<string, UnityEvent> events;
        
        public void Alter(Value alter) {
            Debug.Log("Alter delegate on " + gameObject.name + " triggered!");
            foreach (var d in events) {
                if (alter.AsString.Equals(d.Key)) {
                    Debug.Log("Invoking...");
                    d.Value?.Invoke();
                    break;
                }
            }
        }
    }
    
}