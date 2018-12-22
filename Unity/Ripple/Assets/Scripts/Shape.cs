using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SequenceOrder : short {StartPoint, MiddlePoint, FinishPoint};

abstract public class Shape : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void createObject();
    public abstract void ripple();
}
