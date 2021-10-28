using System.Collections;
using UnityEngine;

namespace WarpSystem {
    public class Warp : MonoBehaviour {
        public Vector3 position;
        public Collider2D bounds;
    
        public void Do(Transform entity, bool IsPlayer, PlayerCamera playerCamera = null)
        {
            if (IsPlayer)
            {
                StartCoroutine(WarpPlayerRoutine(entity, playerCamera));
            } else
            {
                WarpEntity(entity);
            }
        }

        private IEnumerator WarpPlayerRoutine(Transform player, PlayerCamera playerCamera)
        {
            Moveable playerMovement = player.GetComponent<Moveable>();
            playerMovement.PauseMovement();
            yield return StartCoroutine(playerCamera.Fade());
            player.position = position;
            playerCamera.CurrentArea = bounds;
            System.Random rand = new System.Random();
            yield return new WaitForSeconds((float)rand.NextDouble());
            yield return StartCoroutine(playerCamera.Fade(0f));
            playerMovement.CanMove = true;
        }

        private void WarpEntity(Transform entity)
        {
            entity.position = position;
        }
    }
}
