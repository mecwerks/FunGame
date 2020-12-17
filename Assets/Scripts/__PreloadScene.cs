using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class __PreloadScene {
#if UNITY_EDITOR 
    public static int sceneToLoad = -2;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadPreScene() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0) {
            return;
        }

        Debug.Log("Did not start from __preload scene, loading __preload scene now");
        sceneToLoad = sceneIndex;
        SceneManager.LoadScene(0);
    }
#endif
}
