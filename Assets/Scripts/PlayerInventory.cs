using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInventory : NetworkBehaviour {
    [SyncVar]
    List<ItemInformation> items = new List<ItemInformation>();

    [ServerCallback]
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Item")) {
            Item item = other.GetComponent<Item>();

            items.Add(item.information);
            Destroy(other.gameObject, 0);
        }
    }
}
