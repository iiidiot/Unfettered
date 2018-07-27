using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillReleaseController : MonoBehaviour
{

    public GameObject playerIdleMesh;
    public GameObject playerRunMesh;
    public Transform skillRlsP;

    public string idleSkillAnimeName = "M1_IdleMagic";
    public string runSkillAnimeName = "M1_RunMagic";

    public float idleRlsSkillMoment, runRlsSkillMoment;

    public bool isSkillSent = false;

    Animator idleAnimator;
    Animator runAnimator;
    public AnimatorStateInfo idleAnimatorInfo;
    public AnimatorStateInfo runAnimatorInfo;
    public float timeCount = 0;

    // Use this for initialization
    void Start()
    {
        idleAnimator = playerIdleMesh.transform.parent.GetComponent<Animator>();
        runAnimator = playerRunMesh.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        idleAnimatorInfo = idleAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);
        runAnimatorInfo = runAnimator.GetCurrentAnimatorStateInfo(GameInfo.PlayerSkillAnimeLayer);

        if ((idleAnimatorInfo.normalizedTime > idleRlsSkillMoment)
            && (idleAnimatorInfo.IsName(idleSkillAnimeName))
            && !isSkillSent)//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束
        {
            releaseSkill();
        }
        if ((runAnimatorInfo.normalizedTime > runRlsSkillMoment)
            && (runAnimatorInfo.IsName(runSkillAnimeName))
            && !isSkillSent)//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束
        {
            releaseSkill();
        }


        if (GameInfo.BlockPlayerSkillRls)
        {
            timeCount += Time.deltaTime;
        }
        if (timeCount > GameInfo.PlayerRlsSkillCD)
        {
            GameInfo.BlockPlayerSkillRls = false;
            isSkillSent = false;
            timeCount = 0;
        }

    }

    void releaseSkill()
    {
        isSkillSent = true;
        timeCount = 0;

        //release skill!
        FuType ft = GameInfo.battleFuList[GameInfo.PlayerCurrentSkillNum].type;
        GameInfo.battleFuList.RemoveAt(GameInfo.PlayerCurrentSkillNum);

        Fu f = GameInfo.fuList.Find(c => c.type == ft);

        GameInfo.PlayerRlsSkillCD = f.cd;

        Transform swords = GameObject.Find("Root/Swords").transform;

        switch (f.type)
        {
            case FuType.MetalSwordMoveH:
                for (int i = 0; i < swords.childCount; i++)
                {
                    Transform tsf = swords.GetChild(i);
                    MetalSwordMoveH msm = tsf.GetComponent<MetalSwordMoveH>();
                    if (msm) { msm.enabled = true; }
                    
                    msm.beginPrep = true;
                    msm.speed = f.move_speed.x;
                    msm.moveDirection = (transform.position.x - tsf.position.x) > 0 ? 1 : -1;
                    msm.newPos = tsf.position;
                }
                break;

            case FuType.MetalSwordSummon:
                Object skillPreb = Resources.Load(GameInfo.EffectPrefabResDir + f.prefab_path);
                GameObject projectileParticle = Instantiate(skillPreb, skillRlsP.position, skillRlsP.rotation) as GameObject;
                projectileParticle.transform.parent = swords;
                Rigidbody2D pr = projectileParticle.GetComponent<Rigidbody2D>();
                pr.velocity = new Vector2(0, f.move_speed.y);
                break;

            default:
                skillPreb = Resources.Load(GameInfo.EffectPrefabResDir + f.prefab_path);
                projectileParticle = Instantiate(skillPreb, skillRlsP.position, skillRlsP.rotation) as GameObject;
                pr = projectileParticle.GetComponent<Rigidbody2D>();
                pr.velocity = new Vector2(GameInfo.PlayerMoveDirection * f.move_speed.x, f.move_speed.y);
                break;
        }

        //后坐力
        //Rigidbody2D player_r = GetComponent<Rigidbody2D>();
        //player_r.velocity = (new Vector2(-GameInfo.PlayerMoveDirection * 100, 0));
    }
    
}