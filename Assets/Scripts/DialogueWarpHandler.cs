using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarpSystem;
using Yarn.Unity;

[RequireComponent(typeof(Warp))]
public class DialogueWarpHandler : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private Warp _warp;

    void Awake()
    {
        _dialogueManager = GetComponent<DialogueManager>();
        DialogueRunner _dr = _dialogueManager.Runner;
        _warp = GetComponent<Warp>();
        
        _dr.AddCommandHandler(
            "warp",
            WarpPlayer);
    }

    public void WarpPlayer(string[] parameters)
    {
        PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
        Transform player = GameObject.Find("Player").transform;
        float x = float.Parse(parameters[0]);
        float y = float.Parse(parameters[1]);
        _warp.position = new Vector3(x, y);
        _warp.Do(player, true, playerCamera);
    }
}
