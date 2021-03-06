﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Material wordMat;

    public GameObject playerIdleMesh;
    public GameObject playerRunMesh;
    public GameObject idleFire, runFire;//parent

    //set by hand
    public float m_g = 9.81f;
    //public float g_scale = 1.0f;
    public float speed = 1.0f;
    public float sideCollisionThreshold = -0.001f;
    public Transform basePoint, middlePoint, topPoint;

    //====================================

    public Animator idleAnimator;
    public Animator runAnimator;

    public bool isOnGround = true;
    public bool isMoveBlockLeft = false;
    public bool isMoveBlockRight = false;
    public MoveState moveState = MoveState.Idle;

    public float idleJumpAnimeLength;
    public float runJumpAnimeLength;
    public bool jumpBlock = false;
    
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
    void Start()
    {
        //符读取
        //f = Resources.Load<FuScriptableObject>(GameInfo.PersistentResDir + GameInfo.FuListFileName);
        //UnityEditor.Undo.RecordObject(f,"change fulist");
        //f.fuList.Add(new Fu(FuID.FrostBall,"123"));

        idleAnimator = playerIdleMesh.transform.parent.GetComponent<Animator>();
        runAnimator = playerRunMesh.transform.parent.GetComponent<Animator>();
        r = GetComponent<Rigidbody2D>();

        //jump is the second anime clip in animator, so the array index is 1
        idleJumpAnimeLength = idleAnimator.runtimeAnimatorController.animationClips[1].length;
        runJumpAnimeLength = runAnimator.runtimeAnimatorController.animationClips[1].length;

        PlayerIdle();

        string standard_path = Application.streamingAssetsPath + "/Fu/Fire/fireball.jpg";

        //set back word
        Texture2D standard_tx = new Texture2D(Screen.width, Screen.height);
        standard_tx.LoadImage(ImageProcess.getImageByte(standard_path));
        wordMat.mainTexture = standard_tx;
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardControl();
    }

    void FixedUpdate()
    {
        float new_vy = r.velocity.y - m_g * Time.fixedDeltaTime;
        r.velocity = new Vector2(r.velocity.x, new_vy);
    }

    void KeyBoardControl()
    {
        //run
        if (Input.GetAxis("Horizontal") > 0 && !isMoveBlockRight)
        {
            if (isOnGround && moveState != MoveState.RunJump)
            {
                PlayerRun();
            }
            GameInfo.PlayerMoveDirection = 1;
            float s_x = transform.localScale.x;
            if (s_x < 0)
            {
                s_x = -s_x;
            }
            transform.localScale = new Vector3(s_x, transform.localScale.y, transform.localScale.z);

            float speedCo = 1f;
            if (moveState == MoveState.IdleJump)
            {
                speedCo = 0.5f;
            }
            if (moveState == MoveState.RunJump)
            {
                speedCo = 0.77f;
            }
            r.velocity = new Vector2(speedCo * speed * GameInfo.PlayerMoveDirection, r.velocity.y);
        }
        //run on another directon
        if (Input.GetAxis("Horizontal") < 0 && !isMoveBlockLeft)
        {
            if (isOnGround && moveState != MoveState.RunJump)
            {
                PlayerRun();
            }
            GameInfo.PlayerMoveDirection = -1;
            float s_x = transform.localScale.x;
            if (s_x > 0)
            {
                s_x = -s_x;
            }
            transform.localScale = new Vector3(s_x, transform.localScale.y, transform.localScale.z);

            float speedCo = 1f;
            if (moveState == MoveState.IdleJump)
            {
                speedCo = 0.5f;
            }
            if (moveState == MoveState.RunJump)
            {
                speedCo = 0.77f;
            }
            r.velocity = new Vector2(speedCo * speed * GameInfo.PlayerMoveDirection, r.velocity.y);
        }

        //Vector3 move = new Vector3(moveDirection * speed * Time.deltaTime, 0, 0);
        //transform.Translate(move);

        //idle
        //when jump in the air, we don't put any key, it shouldn't be idle
        if (Input.GetAxisRaw("Horizontal") < 0.001f && Input.GetAxisRaw("Horizontal") > -0.001f && isOnGround && moveState != MoveState.IdleJump)
        {
            PlayerIdle();
            r.velocity = new Vector2(0, r.velocity.y);
        }

        //jump
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            jumpBlock = true;
            if (moveState == MoveState.Idle)
            {
                PlayerIdleJump();
            }
            if (moveState == MoveState.Run)
            {
                PlayerRunJump();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpBlock = false;
        }

        //magic 释放符箓
        if (!GameInfo.BlockPlayerSkillRls)
        {
            if(Input.GetButtonDown("Magic1") && GameInfo.battleFuList.Count>0)
            {
                PlayerRlsSkill(0);
            }
            else if (Input.GetButtonDown("Magic2") && GameInfo.battleFuList.Count > 1)
            {
                PlayerRlsSkill(1);
            }
            else if (Input.GetButtonDown("Magic3") && GameInfo.battleFuList.Count > 2)
            {
                PlayerRlsSkill(2);
            }
        }

    }

    void PlayerRlsSkill(int i)
    {
        GameInfo.BlockPlayerSkillRls = true;
        GameInfo.PlayerCurrentSkillNum = i;
        if (moveState == MoveState.Idle || moveState == MoveState.IdleJump)
        {
            idleAnimator.Play("M1_IdleMagic", 1);
        }
        if (moveState == MoveState.Run || moveState == MoveState.RunJump)
        {
            runAnimator.Play("M1_RunMagic", 1);
        }
    }

    void PlayerIdle()
    {
        Run2Idle();

        moveState = MoveState.Idle;

        idleAnimator.SetBool("isIdle", true);
        idleAnimator.SetBool("isIdleJump", false);
    }

    void PlayerIdleJump()
    {
        moveState = MoveState.IdleJump;

        isOnGround = false;

        //v = gt
        r.velocity = new Vector2(r.velocity.x, m_g * idleJumpAnimeLength * 0.5f);

        idleAnimator.SetBool("isIdle", false);
        idleAnimator.SetBool("isIdleJump", true);
    }

    void PlayerRun()
    {
        Idle2Run();

        moveState = MoveState.Run;

        runAnimator.Play("M1_Run");
        runAnimator.SetBool("isRun", true);
        runAnimator.SetBool("isRunJump", false);
    }

    void PlayerRunJump()
    {
        moveState = MoveState.RunJump;

        isOnGround = false;

        //v = gt
        r.velocity = new Vector2(r.velocity.x, m_g * runJumpAnimeLength * 0.5f);

        runAnimator.SetBool("isRun", false);
        runAnimator.SetBool("isRunJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            PlayerIdle();
            isOnGround = true;
            isMoveBlockRight = false;
            isMoveBlockLeft = false;
        }
        else if (collision.gameObject.tag.Equals("Platform"))
        {
            if (moveState == MoveState.Run || moveState == MoveState.Idle)
            {
                isOnGround = true;
            }
            else
            {
                float val = Mathf.Abs(collision.contacts[0].point.x - collision.transform.position.x) - collision.collider.bounds.size.x / 2.0f;
                if (val >= sideCollisionThreshold)//the player is on a side of the platform, not on the platform
                {
                    float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;

                    if (basePoint.position.y < height)//在平台稍下，未在平台上
                    {
                        isOnGround = false;
                    }
                    else//在平台上
                    {
                        PlayerIdle();
                        isOnGround = true;
                        isMoveBlockRight = false;
                        isMoveBlockLeft = false;
                    }
                }
                else//between the 2 sides of the platform
                {
                    float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;
                    if (basePoint.position.y < height)//在平台下，未在平台上
                    {
                        isOnGround = false;
                    }
                    else
                    {
                        PlayerIdle();
                        isOnGround = true;
                        isMoveBlockRight = false;
                        isMoveBlockLeft = false;
                    }
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            if (!isOnGround)//在平台边缘跳跃上平台时，堵塞住左右位移，这样就不会卡在半空的平台墙上
            {
                float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;

                if (basePoint.position.y < height)
                {
                    if (collision.contacts[0].point.x > transform.position.x)//碰撞点在右侧
                    {
                        isMoveBlockRight = true;
                        isMoveBlockLeft = false;
                    }
                    else//碰撞点在左侧
                    {
                        isMoveBlockRight = false;
                        isMoveBlockLeft = true;
                    }
                }
                else
                {
                    isOnGround = true;
                }
            }
            else //on the ground 在平台边缘走，稍微落下一点卡在平台边缘的时候，让他不卡住，fall下来
            {
                float val = Mathf.Abs(collision.contacts[0].point.x - collision.transform.position.x) - collision.collider.bounds.size.x / 2.0f;
                if (val >= sideCollisionThreshold)//the player is on a side of the platform, not on the platform
                {

                    //高大的墙型平台
                    if (middlePoint.position.y >= collision.transform.position.y)
                    {
                        float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;

                        if (basePoint.position.y < height)
                        {
                            isMoveBlockRight = true;
                            isMoveBlockLeft = true;
                            isOnGround = false;
                            runAnimator.Play("M1_Fall");
                        }
                    }

                    //很窄很细的长方形平台
                    else
                    {
                        float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;

                        if (topPoint.position.y > height)
                        {
                            isMoveBlockRight = true;
                            isMoveBlockLeft = true;
                            isOnGround = false;
                            runAnimator.Play("M1_Fall");
                        }
                    }
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            //跳离平台的时候，如果人物最低部位依然比平台高，则判定为从平台向外跳，这时候如果是跑下去的，动画变成fall，如果是跳出去的，不影响
            //falling down start===============

            //platform's height
            float height = collision.transform.position.y + 0.5f * collision.collider.bounds.size.y;

            if (basePoint.position.y > height)
            {
                isMoveBlockRight = false;
                isMoveBlockLeft = false;
                isOnGround = false;
                if (moveState == MoveState.Run)
                {
                    runAnimator.Play("M1_Fall");
                }
            }

            //falling down end===============
        }
    }

    void Idle2Run()
    {
        playerIdleMesh.SetActive(false);
        idleFire.SetActive(false);

        playerRunMesh.SetActive(true);
        runFire.SetActive(true);

        AnimatorStateInfo i = idleAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);
        AnimatorStateInfo r = runAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);

        //AllIdleCollSet(false);
        //AllRunCollSet(true);
    }

    void Run2Idle()
    {
        playerIdleMesh.SetActive(true);
        idleFire.SetActive(true);

        playerRunMesh.SetActive(false);
        runFire.SetActive(false);

        AnimatorStateInfo i = idleAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);
        AnimatorStateInfo r = runAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);

        // AllIdleCollSet(true);
        //AllRunCollSet(false);

    }
}
