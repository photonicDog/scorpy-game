using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueTalkspriter : MonoBehaviour {
    
    public Image talksprite;
    public Animator animator;

    public List<CharacterSpriteSet> sprites;

    private DialogueRunner _dr;

    // Start is called before the first frame update
    void Start() {
        _dr = GetComponent<DialogueRunner>();

        _dr.AddCommandHandler(
            "c",
            UpdateCharacterSprite);

        _dr.AddCommandHandler(
            "focT",
            FocusCameraToTarget);

        _dr.AddCommandHandler(
            "focP",
            FocusCameraToPosition);

        _dr.AddCommandHandler(
            "focD",
            FocusCameraToDisplacement);

        _dr.AddCommandHandler(
            "warp",
            WarpPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateCharacterSprite(string[] parameters) {
        Debug.Log("Sprite updated!");
        Talksprite t = sprites.Find(a => a.name == parameters[0])
                      .sprites.Find(a => a.id == parameters[1]);
        if (t.animated) {
            
        }
        else {
            talksprite.sprite = t.staticSprite;
        }
    }

    public void FocusCameraToTarget(string[] parameters)
    {
        Debug.Log("Moving camera...");
        Transform focus = GameObject.Find(parameters[0]).transform;
        Camera.main.GetComponent<PlayerCamera>().FocusCameraOnTarget(focus);
    }

    public void FocusCameraToPosition(string[] parameters)
    {
        Debug.Log("Moving camera...");
        float x = float.Parse(parameters[0]);
        float y = float.Parse(parameters[1]);
        Camera.main.GetComponent<PlayerCamera>().FocusCameraOnPosition(new Vector3(x,y,0));
    }
    public void FocusCameraToDisplacement(string[] parameters)
    {
        Debug.Log("Moving camera...");
        float x = float.Parse(parameters[0]);
        float y = float.Parse(parameters[1]);
        Camera.main.GetComponent<PlayerCamera>().FocusCameraOnDisplacement(new Vector3(x, y, 0));
    }

    public void WarpPlayer(string[] parameters)
    {
        float x = float.Parse(parameters[0]);
        float y = float.Parse(parameters[1]);
        StartCoroutine(Warp(new Vector3(x, y, 0)));
    }

    private IEnumerator Warp(Vector3 position)
    {
        PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
        yield return StartCoroutine(playerCamera.Fade());
        Transform player = GameObject.Find("Player").transform;
        player.position = position;
        yield return StartCoroutine(playerCamera.Fade());
    }
}
