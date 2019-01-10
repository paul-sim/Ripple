using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeClickHandler : MonoBehaviour {

    bool clickedOnceBefore = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        if (clickedOnceBefore == false) { // prevents clicking on shape multiple times
            clickedOnceBefore = true;
            (GameManager.getInstance()).StartRipple(this.transform.parent.gameObject);
        }
    }
}
