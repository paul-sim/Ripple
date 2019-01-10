using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleCollideHandler : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "CircleMass") {
            GameObject collisionShapeObject = col.transform.parent.gameObject;

            if (collisionShapeObject.GetComponent<Circle>().hasBeenTriggered() == false) { // can only trigger circles that have not yet been triggered
                (GameManager.getInstance()).StartRipple(col.transform.parent.gameObject);
            }
        }
    }
}
