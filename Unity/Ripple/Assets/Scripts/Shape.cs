using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointRole : short {StartPoint, BridgePoint, FinishPoint};

abstract public class Shape : MonoBehaviour {
    
    protected LineRenderer line;
    [SerializeField]
    protected GameObject shapeSpriteObject;
    protected SpriteRenderer shapeSprite;
    [SerializeField]
    protected GameObject rippleColliderObject;
    protected float rippleRadius; // current radius
    protected float rippleRadiusMax; // max ripple reaching distance
    protected int segmentsCount;
    protected float rippleExpandRate;
    protected float spriteFadeRate;
    protected bool triggered;

    // Use this for initialization
    public void Start() {
        line = gameObject.GetComponent<LineRenderer>();
        shapeSprite = GetComponentInChildren<SpriteRenderer>();
        rippleRadius = 0.5f; // start radius
        rippleRadiusMax = 5f;
        rippleExpandRate = 3.0f;
        spriteFadeRate = 2.0f;
        triggered = false;
    }

    public void Ripple() {
        triggered = true;
        StartCoroutine(DrawRipple());
        StartCoroutine(FadeOutShapeSprite());
        StartCoroutine(FadeOutRipple());
    }
    
    IEnumerator FadeOutShapeSprite() {
        while (shapeSprite.color.a > 0) {
            Color spriteColor = shapeSprite.color;
            spriteColor.a -= spriteFadeRate * Time.deltaTime;
            shapeSprite.color = spriteColor;
            yield return new WaitForEndOfFrame(); // this syncs coroutine to frames so we can use Time.deltatime reliably
        }
        Destroy(shapeSpriteObject);
    }

    IEnumerator FadeOutRipple() {
        while (line.startColor.a > 0) {
            Color lineColor = line.startColor;
            lineColor.a = 1 - (rippleRadius / rippleRadiusMax); // the closer the radius reaches rippleRadiusMax, the more opaque it becomes
            line.startColor = line.endColor = lineColor;

            yield return new WaitForEndOfFrame(); // this syncs coroutine to frames so we can use Time.deltatime reliably
        }
        line.enabled = false;
    }

    public bool hasBeenTriggered () {
        return triggered;
    }

    protected abstract IEnumerator DrawRipple();

    protected abstract void UpdateCollisionRipple();

}
