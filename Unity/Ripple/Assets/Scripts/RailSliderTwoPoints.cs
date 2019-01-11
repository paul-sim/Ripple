using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSliderTwoPoints : RailSlider {

    public enum RailSliderPosition : short {one, two};

    [SerializeField]
    public RailSliderPosition currentSliderPosition;

    // Use this for initialization
    new void Start () {
        base.Start();
        sliderPointsAmount = RailSliderPointsAmount.two;
	}

    public override void ToggleSliderPosition() {
        if (currentSliderPosition == RailSliderPosition.one) {
            currentSliderPosition = RailSliderPosition.two;
        }
        else {
            currentSliderPosition = RailSliderPosition.one;
        }
        DrawShapeOnSlider();
    }

    protected override void DrawShapeOnSlider() {
        switch (sliderOrientation) {
            case (RailSliderOrientation.horizontal):
                DrawShapeOnHorizontalSlider();
                break;
            case (RailSliderOrientation.vertical):
                DrawShapeOnVerticalSlider();
                break;
        }        
    }

    void DrawShapeOnHorizontalSlider() {
        switch (currentSliderPosition) {
            case (RailSliderPosition.one):
                attachedShapeObject.transform.localPosition = new Vector3(-1 * slideIncrementDistance, 0, 0);
                break;
            case (RailSliderPosition.two):
                attachedShapeObject.transform.localPosition = new Vector3(slideIncrementDistance, 0, 0);
                break;
            default:
                // nothing happens..
                break;
        }
    }

    void DrawShapeOnVerticalSlider() {
        switch (currentSliderPosition) {
            case (RailSliderPosition.one):
                attachedShapeObject.transform.localPosition = new Vector3(0, slideIncrementDistance, 0);
                break;
            case (RailSliderPosition.two):
                attachedShapeObject.transform.localPosition = new Vector3(0, -1 * slideIncrementDistance, 0);
                break;
            default:
                // nothing happens..
                break;
        }
    }
}
