using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GameManager getInstance() {
        return instance;
    }

    public void StartRipple(GameObject shapeObject) {
        string shape = shapeObject.transform.name;

        switch (shape) {
            case "Circle":
                (shapeObject.GetComponent<Circle>()).Ripple();
                break;
            case "Square":
                break;
        }
    }
}
