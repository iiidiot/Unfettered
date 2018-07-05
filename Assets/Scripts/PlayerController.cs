using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerIdle;
    public GameObject playerRun;

    public float m_g = 9.81f;
    public float speed = 1.0f;
    public int moveDirection = 1;

    public Animator idleAnimator;
    public Animator runAnimator;

    public bool isOnGround = false;
    public MoveState moveState = MoveState.Idle;

    public float idleJumpAnimeLength;
    public float runJumpAnimeLength;

    //enum type   
    public enum MoveState
    {
        Idle, IdleJump, Run, RunJump
    };

    //private
    Rigidbody2D r;

    //================================================================
    //The following is function
    //================================================================

    // Use this for initialization
    void Start () {
        idleAnimator = playerIdle.transform.parent.GetComponent<Animator>();
        runAnimator = playerRun.transform.parent.GetComponent<Animator>();
        r = GetComponent<Rigidbody2D>();

        //jump is the second anime clip in animator, so the array index is 1
        idleJumpAnimeLength = idleAnimator.runtimeAnimatorController.animationClips[1].length;
        runJumpAnimeLength = runAnimator.runtimeAnimatorController.animationClips[1].length;

        PlayerIdle();
	}
	
	// Update is called once per frame
	void Update () {

        //run
        if (isOnGround && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
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


            //Vector3 move = new Vector3(moveDirection * speed * Time.deltaTime, 0, 0);
            //transform.Translate(move);

            r.velocity = new Vector2(speed * moveDirection, r.velocity.y);

        }

        //idle
        if (!Input.anyKey && moveState!=MoveState.IdleJump)
        {
            PlayerIdle();
            r.velocity = new Vector2(0, r.velocity.y);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
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

    void FixedUpdate()
    {
        if (r.gravityScale == 0)
        {
            float new_vy = r.velocity.y - m_g * Time.fixedDeltaTime;
            r.velocity = new Vector2(r.velocity.x, new_vy);
        }
    }

    void PlayerRun()
    {
        r.gravityScale = 1;
        playerIdle.SetActive(false);
        playerRun.SetActive(true);

        moveState = MoveState.Run;
    }

    void PlayerIdle()
    {
        r.gravityScale = 1;
        playerIdle.SetActive(true);
        playerRun.SetActive(false);

        moveState = MoveState.Idle;

        idleAnimator.SetBool("isIdle", true);
        idleAnimator.SetBool("isIdleJump", false);
    }

    void PlayerIdleJump()
    {
        moveState = MoveState.IdleJump;
        isOnGround = false;

        //when jump, block physics gravity, use my own gravity to simulate y move
        r.gravityScale = 0;

        //v = gt
        r.velocity = new Vector2(r.velocity.x, m_g * idleJumpAnimeLength * 0.5f);

        idleAnimator.SetBool("isIdle", false);
        idleAnimator.SetBool("isIdleJump", true);
    }

    void PlayerRunJump()
    {
        moveState = MoveState.RunJump;
        isOnGround = false;

        //when jump, block physics gravity, use my own gravity to simulate y move
        r.gravityScale = 0;

        //v = gt
        r.velocity = new Vector2(r.velocity.x, -Physics2D.gravity.y * runJumpAnimeLength * 0.5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Ground"))
        {
            PlayerIdle();
            isOnGround = true;
        }
    }
}
