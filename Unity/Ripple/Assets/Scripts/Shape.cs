using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointRole : short {StartPoint, BridgePoint, FinishPoint};

abstract public class Shape : MonoBehaviour {
    
    protected LineRenderer line;
    protected SpriteRenderer shapeSprite;
    protected int segmentsCount;
    protected bool rippleStarted;

    // Use this for initialization
    public void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        shapeSprite = GetComponentInChildren<SpriteRenderer>();
        rippleStarted = false;
	}

    // Update is called once per frame
    void Update() {
        if (rippleStarted) {
            DrawRipple();
        }
    }

    // public abstract void createObject();
    public void Ripple() {
        if (rippleStarted == false) {
            rippleStarted = true;
            StartRippleExpansion();
            FadeSpriteAndRipple();
        }
    }

    void StartRippleExpansion() {
        InvokeRepeating("ExpandRipple", 0.0f, 0.005f);
    }

    // make shape sprite and ripple disappear
    void FadeSpriteAndRipple() {
        InvokeRepeating("FadeShapeSprite", 0.0f, 0.05f);
        InvokeRepeating("FadeRipple", 1.0f, 0.05f);
    }

    void FadeShapeSprite() {
        Color spriteColor = shapeSprite.color; 
        spriteColor.a -= .05f; // sprite will disappear over the period of 1 second
        shapeSprite.color = spriteColor;

        if (spriteColor.a <= 0) {
            CancelInvoke("FadeShapeSprite");
        }
        Debug.Log("and..");
    }

    protected abstract void DrawRipple();

    protected abstract void ExpandRipple();

    // make ripple disappear
    protected abstract void FadeRipple();
}
