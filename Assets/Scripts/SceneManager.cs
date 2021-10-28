using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : SerializedMonoBehaviour
{
    public int level = 0;
    public int MainMenuScene = 1;
    public int CreditsScene = 2;
    public int scenesBeforeLevel = 2;

    private static SceneManager _instance;
    public static SceneManager Instance => _instance;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

    public void GoToMainMenu() {
        SwitchScene(MainMenuScene);
    }

    public void GoToCredits() {
        SwitchScene(CreditsScene);
    }

    public void SwitchLevel(int target) {
        SwitchScene(target+scenesBeforeLevel);
    }

    void SwitchScene(int i) {
        StartCoroutine(LoadScene(i));
    }

    IEnumerator LoadScene(int i) {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 0) {
            AsyncOperation op =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            op.allowSceneActivation = true;
            yield return op;
        } else {
            yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentScene);
            
            if (CameraFader.Instance) {
                yield return StartCoroutine(CameraFader.Instance.FadeCoroutine(1f, 1f));
                AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
                yield return op;
                yield return StartCoroutine(CameraFader.Instance.FadeCoroutine(0f, 1f));
            } 
            else {
                AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
                yield return op;
            }
        }
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager
            .GetSceneByBuildIndex(i));
    }
}
