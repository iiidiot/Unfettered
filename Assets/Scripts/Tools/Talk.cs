using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour {
    public string NPC_name;
    public GameObject dialog;
    public Camera cam;
    public float Y_offset = 80;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            dialog.SetActive(true);
        }
        dialog.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(transform.position) + new Vector3(0, Y_offset, 0);
    }

    void LateUpdate()
    {
        //dialog.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(transform.position) + new Vector3(0, Y_offset, 0);
    }


    //============================for NPC===========================================
    //// 碰撞开始
    //void OnTriggerEnter(Collider col)
    //{
    //    //print("OnTriggerEnter");
    //    if (col.gameObject.name.Equals(NPC_name))
    //    {
    //        //print(NPC_name);
    //        dialog.SetActive(true);

    //    }
    //}

    //// 碰撞结束
    //void OnTriggerExit(Collider col)
    //{
    //    if (col.gameObject.name.Equals(NPC_name))
    //    {
    //        //print("OnCollisionExit");
    //        dialog.SetActive(false);
    //    }
    //}
}
