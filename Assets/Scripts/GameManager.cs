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
    public Cinemachine.CinemachineFreeLook FreeLookCam;
    private bool initialized = false;

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
#endif
    }

    public void SetupLocalPlayer(GameObject localPlayer, Transform lookAtTarget)
    {
        Transform camTransform = FindObjectOfType<Camera>().transform;
        localPlayer.GetComponent<PlayerMovement>().Cam = camTransform;
        localPlayer.GetComponent<PlayerAttack>().Cam = camTransform;
        FreeLookCam.LookAt = lookAtTarget;
        FreeLookCam.Follow = localPlayer.transform;
    }
}
