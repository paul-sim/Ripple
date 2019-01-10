using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape {

    protected CircleCollider2D rippleCollider;

    // Use this for initialization
    new void Start () {
        base.Start();
        segmentsCount = 100; // number of points used to draw circle
        line.positionCount = segmentsCount + 1;
        rippleCollider = rippleColliderObject.GetComponent<CircleCollider2D>();
        rippleCollider.radius = 0;
        rippleCollider.enabled = false;
    }

    protected override IEnumerator DrawRipple() {
        rippleCollider.enabled = true;

        while (rippleRadius < rippleRadiusMax) {
            DrawCircle();
            CalculateNewRadius();
            UpdateCollisionRipple();
            yield return new WaitForEndOfFrame(); // this syncs coroutine to frames so we can use Time.deltatime reliably
        }
    }
    
    void DrawCircle() {
        line.positionCount = segmentsCount + 1;
        float x;
        float y;
        float z = 0f;
        float angle = 20f;
        Vector3[] points = new Vector3[segmentsCount + 1];

        for (int i = 0; i < (segmentsCount + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * rippleRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * rippleRadius;

            points[i] = new Vector3(x, y, z);
            angle += (360f / segmentsCount);
        }
        line.SetPositions(points);
    }

    void CalculateNewRadius() {
        rippleRadius += Time.deltaTime * rippleExpandRate;
        if (rippleRadius > rippleRadiusMax) {
            rippleRadius = rippleRadiusMax;
        }
    }

    protected override void UpdateCollisionRipple() {
        rippleCollider.radius = rippleRadius;
        if (rippleCollider.radius == rippleRadiusMax) {
            Destroy(rippleColliderObject);
        }
    }
}
