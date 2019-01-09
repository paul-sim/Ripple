using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeClickHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        (GameManager.getInstance()).StartRipple(this.transform.parent.gameObject);
    }
}
