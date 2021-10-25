using System.Collections;
using UnityEngine;

namespace WarpSystem {
    public class Warp : MonoBehaviour {
        public Vector3 position;
        public Collider2D bounds;
    
        public void Do(Transform player, PlayerCamera playerCamera)
        {
            StartCoroutine(WarpRoutine(player, playerCamera));
        }

        private IEnumerator WarpRoutine(Transform player, PlayerCamera playerCamera)
        {
            Moveable playerMovement = player.GetComponent<Moveable>();
            playerMovement.PauseMovement();
            yield return StartCoroutine(playerCamera.Fade());
            player.position = position;
            playerCamera.CurrentArea = bounds;
            System.Random rand = new System.Random();
            yield return new WaitForSeconds((float)rand.NextDouble());
            yield return StartCoroutine(playerCamera.Fade());
            playerMovement.CanMove = true;
        }
    }
}
