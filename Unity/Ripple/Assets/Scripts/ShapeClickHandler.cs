using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to a ShapeMass (e.g. CircleMass) object
public class ShapeClickHandler : MonoBehaviour {

    GameObject thisShapeObject;
    PointRole thisPointRole; // enum defined in Shape.cs
 
	void Start () {
        thisShapeObject = this.transform.parent.gameObject;
        thisPointRole = thisShapeObject.GetComponent<Shape>().getPointRole();
    }

    void OnMouseDown() {

        switch (thisPointRole) {
            case (PointRole.StartPoint):
                if (thisShapeObject.GetComponent<Shape>().hasBeenTriggered() == false) {
                    (GameManager.getInstance()).StartRipple(thisShapeObject);
                }
                break;
            case (PointRole.BridgePoint):
                if (HasRailSlider()) {
                    ToggleRailSlider();
                }
                break;
            case (PointRole.FinishPoint):
                if (HasRailSlider()) {
                    ToggleRailSlider();
                }
                break;
        }        
    }

    bool HasRailSlider() {
        if (thisShapeObject.GetComponent<Shape>().hasRailSlider()) {
            return true;
        }
        return false;
    }

    void ToggleRailSlider() {
        RailSlider railSlider = thisShapeObject.GetComponent<Shape>().getAttachedRailSlider();
        GameManager.getInstance().ToggleRailSliderPosition(railSlider);
    }
}
