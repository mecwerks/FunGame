using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

//[assembly: AlwaysLinkAssembly]

public class GameManager : Singleton<GameManager>
{
    public GameObject IngameMenuPrefab;
    public Cinemachine.CinemachineFreeLook FreeLookCam;
    private bool initialized = false;

    private GameObject IngameMenuObject;

    protected GameManager() { } // Prevent non-singleton constructor use.

    private void Awake()
    {
        gameObject.AddComponent<Consolation.Console>();

#if UNITY_EDITOR 
        if (__PreloadScene.sceneToLoad > 0)
        {
            Debug.Log("Returning again to the scene: " + __PreloadScene.sceneToLoad);
            SceneManager.LoadScene(__PreloadScene.sceneToLoad);
        }
        else
#endif
        {
            SceneManager.LoadScene(1);
        }

        // load menus
        // disable all but active one
        // handle scene switching and menu swapping
        IngameMenuObject = Instantiate(IngameMenuPrefab, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(IngameMenuObject);
    }

    public void SetupLocalPlayer(GameObject localPlayer, Transform lookAtTarget)
    {
        Transform camTransform = FindObjectOfType<Camera>().transform;
        localPlayer.GetComponent<PlayerMovement>().Cam = camTransform;
        localPlayer.GetComponent<PlayerAttack>().Cam = camTransform;
        FreeLookCam.LookAt = lookAtTarget;
        FreeLookCam.Follow = localPlayer.transform;

        IngameMenuObject.GetComponent<IngameMenu>().AssignLocalPlayer(localPlayer.GetComponent<Damageable>());
    }
}
