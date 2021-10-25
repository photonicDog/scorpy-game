using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public abstract class Collideable : MonoBehaviour {
    protected DialogueRunner _dr;
    protected Transform _player;
    protected PlayerCamera _playerCamera;

    private void Awake() {
        _dr = DialogueManager.Instance.Runner;
        _player = GameObject.Find("Player").transform;
        _playerCamera = Camera.main.GetComponent<PlayerCamera>();
    }

    public abstract void Activate();
}
