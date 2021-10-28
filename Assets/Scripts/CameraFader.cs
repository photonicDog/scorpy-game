using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    private static CameraFader _instance;
    public static CameraFader Instance {
        get { return _instance; }
    }
    public float fadeValue;
    public Image fadePane;
    
    void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(this);
        }
        else {
            _instance = this;
        }

        fadeValue = 0;
        
        fadePane.material.color = 
            new Color(
                fadePane.material.color.r, 
                fadePane.material.color.g, 
                fadePane.material.color.b, 
                fadeValue);
    }

    public IEnumerator FadeCoroutine(float target, float time) {
        float startValue = fadeValue;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            elapsedTime += Time.deltaTime;
            fadePane.material.color = 
                new Color(
                    fadePane.material.color.r, 
                    fadePane.material.color.g, 
                    fadePane.material.color.b, 
                    Mathf.Lerp(startValue, target, elapsedTime/time)
                    );
            yield return null; 
        }
    }
}
