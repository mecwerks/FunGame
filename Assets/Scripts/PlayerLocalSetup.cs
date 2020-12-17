using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerLocalSetup : NetworkBehaviour {
    public Transform CameraLookAtTarget;

    public override void OnStartClient() {
        if (isLocalPlayer) {
            GameManager.Instance.SetupLocalPlayer(gameObject, CameraLookAtTarget);
        }
    }
}
