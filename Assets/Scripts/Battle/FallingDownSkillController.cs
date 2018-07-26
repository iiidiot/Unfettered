using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDownSkillController : MonoBehaviour
{

    public GameObject impactParticle;

    public float height;//from player position

    Rigidbody2D r;
    bool hasCollided = false;
    bool isUsed = false;
    

    // Use this for initialization
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        transform.Translate(5, height, 0);

        //todo 自动索敌
    }

    // Update is called once per frame
    void Update()
    {
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
            print(1);
                r.velocity = Vector2.zero;
                r.gravityScale = 0;
            float ySize = hit.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.y * hit.transform.localScale.y;
            float hitY = hit.transform.position.y + ySize/2;
            Vector3 hitPosition = new Vector3(transform.position.x, hitY, transform.position.z);
                impactParticle = Instantiate(impactParticle, hitPosition, impactParticle.transform.rotation) as GameObject;
                Destroy(impactParticle, 3f);
            }
        //}
    }
}
