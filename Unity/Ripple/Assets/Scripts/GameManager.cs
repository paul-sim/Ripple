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

    public static GameManager getInstance() {
        return instance;
    }

    // should only take shape objects in the parameter else errors..
    public void StartRipple(GameObject shapeObject) {
        shapeObject.GetComponent<Shape>().Ripple();
    }

    public void ToggleRailSliderPosition(RailSlider railSliderObject) {
        railSliderObject.ToggleSliderPosition();
    }
}
