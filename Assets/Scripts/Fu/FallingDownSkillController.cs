using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDownSkillController : MonoBehaviour
{

    public GameObject impactParticle;

    public float height;//from player position

    public Transform target;

    public float switchTime = 2.0f;//change to another operate mode

    Rigidbody2D r;
    bool hasCollided = false;
    bool isUsed = false;
    bool isBlockTimer = false;
    

    // Use this for initialization
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        
        Transform m = GameObject.Find("Enemys").transform;
        if(m.childCount >0)
        {
            target = m.GetChild(Random.Range(0, m.childCount - 1));
        }
        else
        {
            target = GameObject.Find("Player").transform;
        }

        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlockTimer)
        {
            StartCoroutine("m_timer");
        }
    }

    IEnumerator m_timer()
    {
        isBlockTimer = true;
        yield return new WaitForSeconds(switchTime);
        MetalSwordOverScreenBack msosb = GetComponent<MetalSwordOverScreenBack>();
        if (msosb)
        {
            msosb.enabled = true;
        }
        FallingDownSkillController fdsc = GetComponent<FallingDownSkillController>();
        fdsc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        //if (hit.gameObject.tag == "Enemy")
        //{
        //    isUsed = true;
        //}
        //if(isUsed)
        //{
            if (hit.gameObject.tag == "Ground" || hit.gameObject.tag == "Platform")
            {
                r.velocity = Vector2.zero;
                r.gravityScale = 0;
            float ySize = hit.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.y * hit.transform.localScale.y;
            float hitY = hit.transform.position.y + ySize/2;
            Vector3 hitPosition = new Vector3(transform.position.x, hitY, transform.position.z);
            if (impactParticle)
            {
                impactParticle = Instantiate(impactParticle, hitPosition, impactParticle.transform.rotation) as GameObject;
                Destroy(impactParticle, 3f);
            }
            }
        //}
    }
}
