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
    private Transform MainCam;
    private Cinemachine.CinemachineFreeLook FreeLookCam;
    private bool initialized = false;

    protected GameManager() { } // Prevent non-singleton constructor use.

    [RuntimeInitializeOnLoadMethod]
    [SuppressMessage("Code Quality", "IDE0051")]
    static void Initialize()
    {
        // the first call to Instance, causes a new gameobject to be created
        if (Instance.initialized)
        {
            return;
        }

        // Need to load these as resources, otherwise unity omits them from the build
        GameObject thirdPersonCamPrefab = Resources.Load<GameObject>("Third Person Camera");
        GameObject netObjPrefab = Resources.Load<GameObject>("NetworkObject");
        GameObject tpcObj = Instantiate(thirdPersonCamPrefab, new Vector3(0, 10f, 0), Quaternion.identity);

        Instantiate(netObjPrefab, Instance.transform);
        Instance.MainCam = FindObjectOfType<Camera>().transform;
        Instance.FreeLookCam = tpcObj.GetComponent<Cinemachine.CinemachineFreeLook>();
        Instance.gameObject.AddComponent<Consolation.Console>();

        Instance.initialized = true;
    }

    public void SetupLocalPlayer(GameObject localPlayer, Transform lookAtTarget)
    {
        localPlayer.GetComponent<PlayerMovement>().Cam = MainCam;
        localPlayer.GetComponent<PlayerAttack>().Cam = MainCam;
        FreeLookCam.LookAt = lookAtTarget;
        FreeLookCam.Follow = localPlayer.transform;
    }
}
