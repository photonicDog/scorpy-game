using Assets.Scripts.WarpSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WarpSystem {
    [RequireComponent(typeof(Warp))]
    public class WarpableObject : Interactable, IWarpable
    {
        private Warp _warp;
        public Warp Warp { get => _warp; set => _warp = value; }

        public override void Awake() {
            base.Awake();
            Warp = GetComponent<Warp>();
        }

        public override void Interact() {
            Warp.Do(_player, true, _playerCamera);
        }
    }
}