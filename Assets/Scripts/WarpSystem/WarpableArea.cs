using UnityEngine;

namespace WarpSystem {
    [RequireComponent(typeof(Warp))]
    public class WarpableArea : Collideable
    {
        private Warp _warp;

        public override void Awake() {
            base.Awake();
            _warp = GetComponent<Warp>();
        }

        public override void Activate() {
            _warp.Do(_player, _playerCamera);
        }
    }
}
