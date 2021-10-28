using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class HideableObject : Interactable
    {
        public Sprite Open;
        public Sprite Closed;

        public bool Occupied;
        private SpriteRenderer _spRender;
        private Player _playerScript;
        private Vector3 _enteredLocation;

        private void Start()
        {
            _playerScript = _player.GetComponent<Player>();
            _spRender = GetComponent<SpriteRenderer>();
        }

        public override void Interact()
        {
            if (!Occupied)
            {
                _playerScript.Hide();
                _enteredLocation = _player.position;
                _player.position = transform.position;
                _spRender.sprite = Closed;
                Occupied = true;
                _playerCamera.FocusCameraOnTarget(transform);
                StartCoroutine(_playerCamera.Fade(0.9f));
            } else
            {
                _playerScript.Unhide();
                _player.position = _enteredLocation;
                _spRender.sprite = Open;
                Occupied = false;
                _playerCamera.UnlockCamera();
                StartCoroutine(_playerCamera.Fade(0f));
            }
        }

        public void ForceOpen()
        {
            if(Occupied)
            {
                _player.position = transform.position;
                _playerScript.Unhide();
                _playerScript.DisableMovement();
            }
            _spRender.sprite = Open;
        }
    }
}
