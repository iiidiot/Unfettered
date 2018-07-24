using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
    public Vector3 offset;
    public float co = 2.0f;
    public float smooth = 1.0f;

	// Use this for initialization
	void Start () {
        offset = transform.position - followTarget.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //x 跟随
        transform.position = new Vector3((followTarget.position + offset).x, transform.position.y, transform.position.z);


        //y 平滑跟上
        float newCamY = (followTarget.position + offset).y;
        float deltaY = (newCamY - transform.position.y) / co;
        //transform.Translate(0, deltaY, 0);
        transform.position = Vector3.Lerp(transform.position, followTarget.position + offset, Time.deltaTime * smooth);
    }
}
