using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointRole : short {StartPoint, BridgePoint, FinishPoint};

abstract public class Shape : MonoBehaviour {
    
    protected LineRenderer line;
    [SerializeField]
    protected GameObject shapeMassObject;
    [SerializeField]
    protected GameObject rippleColliderObject;
    [SerializeField]
    public PointRole pointRole;
    [SerializeField]
    private RailSlider attachedRailSlider;
    protected float rippleRadius; // current radius
    protected float rippleRadiusMax; // max ripple reaching distance
    protected int segmentsCount;
    protected float rippleExpandRate;
    protected float spriteFadeRate;
    protected bool triggered;

    // Use this for initialization
    public void Start() {
        line = gameObject.GetComponent<LineRenderer>();
        rippleRadius = 0.3f; // start radius
        rippleRadiusMax = 2.5f;
        rippleExpandRate = 2.0f;
        spriteFadeRate = 2.0f;
        triggered = false;
    }

    public void Ripple() {
        triggered = true;
        StartCoroutine(DrawRipple());
        StartCoroutine(FadeOutShapeMass());
        StartCoroutine(FadeOutRipple());
    }
    
    IEnumerator FadeOutShapeMass() {
        SpriteRenderer shapeSprite = GetComponentInChildren<SpriteRenderer>();
        // if attached to a rail slider, fade that out too
        if (attachedRailSlider != null) {
            attachedRailSlider.startFadeOutRailCoroutine();
        }

        while (shapeSprite.color.a > 0) {
            Color spriteColor = shapeSprite.color;
            spriteColor.a -= spriteFadeRate * Time.deltaTime;
            shapeSprite.color = spriteColor;
            yield return new WaitForEndOfFrame(); // this syncs coroutine to frames so we can use Time.deltatime reliably
        }

        Destroy(shapeMassObject);
        // if this shape was a finish point, player has beat the level
        if (pointRole == PointRole.FinishPoint) {
            Debug.Log("You beat this level.");
        }
    }

    IEnumerator FadeOutRipple() {
        float startFadeMarkPercent = 0.5f;
        float startFadeMarkDistance = startFadeMarkPercent * rippleRadiusMax;
        
        while ((rippleRadius / rippleRadiusMax) < startFadeMarkPercent) { // keep ripple alpha at 1 until it reaches a certain distance
            yield return new WaitForEndOfFrame();
        }

        while (line.startColor.a > 0) {
            
            Color lineColor = line.startColor;

            lineColor.a = 1 - ((rippleRadius - startFadeMarkDistance) / (rippleRadiusMax - startFadeMarkDistance)); // the closer the radius reaches rippleRadiusMax, the more opaque it becomes
            line.startColor = line.endColor = lineColor;

            yield return new WaitForEndOfFrame();
        }
        line.enabled = false;
    }

    public bool hasBeenTriggered () {
        return triggered;
    }

    public PointRole getPointRole() {
        return pointRole;
    }
    
    public bool hasRailSlider() {
        if (attachedRailSlider != null) {
            if (attachedRailSlider.attachedShapeObject == this.gameObject) {
                return true;
            }
        }
        return false;
    }

    public RailSlider getAttachedRailSlider() {
        return attachedRailSlider;
    }

    public float getSpriteFadeRate() {
        return spriteFadeRate;
    }

    protected abstract IEnumerator DrawRipple();

    protected abstract void UpdateCollisionRipple();

}
