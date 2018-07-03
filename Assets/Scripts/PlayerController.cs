using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerIdle;
    public GameObject playerRun;

    public float speed = 1.0f;
    public int moveDirection = 1;

    public Animator idleAnimator;
    public Animator runAnimator;

    public bool isOnGround = true;
    public MoveState moveState = MoveState.Idle;

    //enum type   
    public enum MoveState
    {
        Idle, IdleJump, Run, RunJump
    };

    //================================================================
    //The following is function
    //================================================================

    // Use this for initialization
    void Start () {
        idleAnimator = playerIdle.GetComponent<Animator>();
        runAnimator = playerRun.GetComponent<Animator>();

        PlayerIdle();
	}
	
	// Update is called once per frame
	void Update () {

        //run
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            PlayerRun();
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                moveDirection = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            

            Vector3 move = new Vector3(moveDirection * speed * Time.deltaTime, 0, 0);

            transform.Translate(move);
        }

        //idle
        if (!Input.anyKey)
        {
            PlayerIdle();
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && GameInfo.PlayerGlobalInfo.placeState != GameInfo.ChPlaceState.InAir)
        {
           if(GameInfo.PlayerGlobalInfo.moveState == GameInfo.ChMoveState.Idle)
            {
                PlayerIdleJump();
            }
           else if(GameInfo.PlayerGlobalInfo.moveState == GameInfo.ChMoveState.Run)
            {
                PlayerRunJump();
            }
        }

    }

    void PlayerRun()
    {
        playerIdle.SetActive(false);
        playerRun.SetActive(true);

        moveState = MoveState.Run;
        isOnGround = true;
    }

    void PlayerIdle()
    {
        playerIdle.SetActive(true);
        playerRun.SetActive(false);

        moveState = MoveState.Idle;
        isOnGround = true;
    }

    void PlayerIdleJump()
    {
        moveState = MoveState.IdleJump;
        isOnGround = false;
    }

    void PlayerRunJump()
    {
        moveState = MoveState.RunJump;
        isOnGround = false;
    }
}
