using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Flags {
    public class FlagManager : MonoBehaviour {
        public FlagSet flags;
        private FlagSet _flags;

        private static FlagManager _instance;
        public static FlagManager Instance {
            get { return _instance; }
        }

        private void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this);
            }
            else {
                _instance = this;
            }
            
            _flags = Instantiate(flags);
        }

        public void Reset() {
            Destroy(_flags);
            _flags = Instantiate(flags);
        }

        public void SetFlag(string id, Yarn.Value v) {
            _flags.SetFlag(id, v);
        }

        public object GetFlag(string id) {
            return _flags.GetFlag(id);
        }
    }
}

