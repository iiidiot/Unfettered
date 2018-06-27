using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleController : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
      
    }
	
	// Update is called once per frame
	void Update () {
        InputControl();
	}

    void InputControl()
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.Space) && animatorInfo.IsName("M1_Idle"))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isIdleJump", true);
        }

        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("M1_IdleJump")))
        {
            animator.SetBool("isIdleJump", false);
            animator.SetBool("isIdle", true);
        }
    }
}
