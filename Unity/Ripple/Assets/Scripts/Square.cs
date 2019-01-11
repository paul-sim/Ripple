using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape {

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override IEnumerator DrawRipple() {
        yield return null;
    }

    protected override void UpdateCollisionRipple() {

    }
}
