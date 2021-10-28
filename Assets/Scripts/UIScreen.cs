using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIScreen : SerializedMonoBehaviour {
    public GameObject cursor;

    public delegate void UIAction();
    [NonSerialized][OdinSerialize]public Dictionary<int, Tuple<Vector2, UIAction>> Actions;

    private int position;

    private AudioSource _audio;

    public AudioClip SelectAudio;
    public AudioClip StartAudio;

    public void Awake() {
        position = 0;
    }

    public void Start() {
        ControlManager.Instance.SetControlUI();
        ControlManager.Instance.controls.UI.Move.performed += Select;
        ControlManager.Instance.controls.UI.Confirm.performed += Do;
        _audio = GetComponent<AudioSource>();

    }
    public void StartChime() {
        _audio.clip = StartAudio;
        _audio.Play();
    } 
    
    public void Select(InputAction.CallbackContext context) {
        _audio.clip = SelectAudio;
        _audio.Play();
        SetCursor(context.ReadValue<Vector2>().y);
    }

    private void SetCursor(float direction) {
        if (direction < 0) {
            position++;
        }

        if (direction > 0) {
            position--;
        }

        if (position >= Actions.Count) position = 0;
        if (position < 0) position = Actions.Count - 1;
        UpdatePosition();
    }

    private void UpdatePosition() {
        cursor.transform.localPosition = Actions[position].Item1;
    }

    public void Do(InputAction.CallbackContext context) {
        Actions[position].Item2.Invoke();
        StartChime();
    }
}
