using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
