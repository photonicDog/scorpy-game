using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Flags {
    public class FlagManager : MonoBehaviour {
        public FlagSet flags;
        private FlagSet _flags;

        private void Awake() {
            _flags = Instantiate(flags);
        }

        public void Reset() {
            Destroy(_flags);
            _flags = Instantiate(flags);
        }

        public void SetFlag(string id, int v) {
            _flags.SetFlag(id, v);
        }
        
        public void SetFlag(string id, string v) {
            _flags.SetFlag(id, v);
        }
        
        public void SetFlag(string id, bool v) {
            _flags.SetFlag(id, v);
        }

        public object GetFlag(string id) {
            return _flags.GetFlag(id);
        }
    }
}

