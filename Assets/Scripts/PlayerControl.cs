using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed = 1;
    public float jumpForce = 1;
    public float jumpSpeed = 1;
    public bool isInAir = false;
    public string groundStr = "Ground";
    public Rigidbody rig;

	// Use this for initialization
	void Start () {
        //rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        int dir = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
        }

        float translation = dir * speed * Time.deltaTime;
        transform.Translate(translation, 0, 0);

        //print("Y: "+transform.position.y);

    }

    void FixedUpdate()
    {
        if (rig)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
            {
                rig.velocity += new Vector3(0, jumpSpeed, 0); //添加加速度
                //rig.AddForce(Vector3.up * jumpForce);
            }
        }
        else
        {
            print("no rigid body for player!");
        }
    }

    // 碰撞开始
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(groundStr))
        {
            //print("OnCollisionEnter");
            isInAir = true;
        }
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals(groundStr))
        {
            //print("OnCollisionExit");
            isInAir = true;
        }
    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {
        //print("stay");
        if (collision.gameObject.tag.Equals(groundStr))
        {
            //print("OnCollisionExit");
            isInAir = false;
        }
    }
}
