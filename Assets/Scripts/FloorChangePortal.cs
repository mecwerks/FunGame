using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FloorChangePortal : NetworkBehaviour {
    public Transform target;

    private void OnTriggerStay(Collider other) {
        if (!isServer) {
            return;
        }

        if (!other.CompareTag("Player")) {

            return;
        }

        if (GameManager.Instance.isFloorCleared) {
            // TODO make this a nice transition
            other.GetComponent<PlayerMovement>().TeleportTo(target.position, target.rotation);
        }
    }
}
