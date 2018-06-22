using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - followTarget.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = followTarget.position + offset;
		
	}
}
