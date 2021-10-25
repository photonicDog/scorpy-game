using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class WarpableArea : Collideable
    {
        public Vector3 position;
        public Collider2D bounds;

        public override void Activate()
        {
            StartCoroutine(Warp());
        }

        private IEnumerator Warp()
        {
            Moveable playerMovement = _player.GetComponent<Moveable>();
            playerMovement.CanMove = false;
            playerMovement.Stop();
            yield return StartCoroutine(_playerCamera.Fade());
            _player.position = position;
            _playerCamera.CurrentArea = bounds;
            System.Random rand = new System.Random();
            yield return new WaitForSeconds((float)rand.NextDouble());
            yield return StartCoroutine(_playerCamera.Fade());
            playerMovement.CanMove = true;
        }
    }
}
