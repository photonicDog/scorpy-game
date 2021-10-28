using Assets.Scripts.WarpSystem;
using UnityEngine;

namespace WarpSystem {
    [RequireComponent(typeof(Warp))]
    public class WarpableArea : Collideable, IWarpable
    {
        private Warp _warp;
        public Warp Warp { get => _warp; set => _warp = value; }

        public override void Awake() {
            base.Awake();
            Warp = GetComponent<Warp>();
        }

        public override void Activate() {
            Warp.Do(_player, true, _playerCamera);
        }
    }
}
