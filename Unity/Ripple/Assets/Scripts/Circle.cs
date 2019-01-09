using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape {
    
    float radius;

    // Use this for initialization
    new void Start () {
        base.Start();
        segmentsCount = 100;
        line.positionCount = segmentsCount + 1;
        radius = 0.1f;
    }

    protected override void DrawRipple() {
        DrawCircle();
    }

    protected override void ExpandRipple() {
        radius += 0.008f;
    }

    void DrawCircle() {

        //line.positionCount = 0;
        if (segmentsCount < 0) {
            segmentsCount = 0;
        }
        line.positionCount = segmentsCount + 1;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segmentsCount + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segmentsCount);
        }
    }

    protected override void FadeRipple() {
        FadeCircle();
    }

    void FadeCircle() {
        Color lineColor = line.startColor; // sprite will disappear over the period of 2 seconds after the 1 second delay
        lineColor.a -= .025f;
        line.startColor = lineColor;
        line.endColor = lineColor;

        if (lineColor.a <= 0) {
            CancelInvoke("FadeCircle");
        }
    }
}
