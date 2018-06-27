using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerIdle;
    public GameObject playerRun;
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
        PlayerIdle();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerRun();
        }
        if (Input.GetKey(KeyCode.D))
        {
            

            Vector3 move = new Vector3(speed * Time.deltaTime, 0, 0);

            transform.Translate(move);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            PlayerIdle();
        }

    }

    void PlayerRun()
    {
        playerIdle.SetActive(false);
        playerRun.SetActive(true);
    }

    void PlayerIdle()
    {
        playerIdle.SetActive(true);
        playerRun.SetActive(false);
    }
}
