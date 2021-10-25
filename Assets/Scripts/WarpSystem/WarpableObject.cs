using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WarpSystem {
    [RequireComponent(typeof(Warp))]
    public class WarpableObject : Interactable {
        private Warp _warp;

        public override void Awake() {
            base.Awake();
            _warp = GetComponent<Warp>();
        }

        public override void Interact() {
            _warp.Do(_player, _playerCamera);
        }
    }
}