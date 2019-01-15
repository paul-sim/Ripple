using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RailSliderPointsAmount : short {two, three, four };
public enum RailSliderOrientation : short {horizontal, vertical};

// [System.Serializable]
[ExecuteInEditMode]
abstract public class RailSlider : MonoBehaviour {

    protected float slideIncrementDistance; // sliding distance for shape movement
    protected RailSliderPointsAmount sliderPointsAmount;
    [SerializeField]
    public RailSliderOrientation sliderOrientation;
    [SerializeField]
    public GameObject attachedShapeObject;
    
    public void Start () {
        slideIncrementDistance = 1.0f;
        attachedShapeObject.GetComponent<Shape>().getSpriteFadeRate();
	}
	
	void Update () {
        #if UNITY_EDITOR
        // note, this changes only the visuals for the slider orientation. Has no actual technical significance
        switch (sliderOrientation) {
            case RailSliderOrientation.horizontal:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case RailSliderOrientation.vertical:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
        }

        DrawShapeOnSlider();
        #endif
	}

    public void startFadeOutRailCoroutine() {
        StartCoroutine(FadeOutRailSlider());
    }

    IEnumerator FadeOutRailSlider() {
        SpriteRenderer railSprite = GetComponent<SpriteRenderer>();
        float spriteFadeRate = attachedShapeObject.GetComponent<Shape>().getSpriteFadeRate();

        while (railSprite.color.a > 0) {
            Color spriteColor = railSprite.color;
            spriteColor.a -= spriteFadeRate * Time.deltaTime;
            railSprite.color = spriteColor;
            yield return new WaitForEndOfFrame(); // this syncs coroutine to frames so we can use Time.deltatime reliably
        }
        Destroy(gameObject);
    }

    public abstract void ToggleSliderPosition();

    protected abstract void DrawShapeOnSlider();
}
