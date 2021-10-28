using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueCameraHandler : MonoBehaviour {
    private DialogueManager _dialogueManager;
    
    // Start is called before the first frame update
    void Awake() {
        _dialogueManager = GetComponent<DialogueManager>();
        DialogueRunner _dr = _dialogueManager.Runner;
        
        _dr.AddCommandHandler(
            "focT",
            FocusCameraToTarget);

        _dr.AddCommandHandler(
            "focP",
            FocusCameraToPosition);

        _dr.AddCommandHandler(
            "focD",
            FocusCameraToDisplacement);
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
}
