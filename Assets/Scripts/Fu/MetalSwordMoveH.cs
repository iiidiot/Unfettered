using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSwordMoveH : MonoBehaviour
{

    public float prepTime = 1f;//prepare time
    public int moveDirection = 1;
    public float speed = 0;
    public string muzzlePrefabPath;

    public Vector3 newPos;
    public float timeCount = 0;
    public bool beginPrep = false;
    public bool beginMove = false;
    public Rigidbody2D r;
    public int dir = 3;//1 left 2right 3up 4down

    // Use this for initialization
    void Start()
    {
        timeCount = 0;
        r = GetComponent<Rigidbody2D>();
        //Vector2 curV = - new Vector2(transform.up.x, transform.up.y);
        //Vector2 toV = new Vector2(moveDirection, 0);
        //theta = Mathf.Acos(curV.x * toV.x + curV.y * toV.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (beginPrep)
        {
            r.velocity = Vector2.zero;
            timeCount += Time.deltaTime * prepTime;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * prepTime);
            //transform.up = Vector3.Lerp(m_up, -new Vector3(moveDirection, 0, 0), timeCount);
            float rotateZ = 0;
            switch (dir)
            {
                case 1://left
                    rotateZ = moveDirection > 0 ? Mathf.Lerp(-90f, 90f, timeCount) : -90f;
                    break;
                case 2://right
                    rotateZ = moveDirection < 0 ? Mathf.Lerp(90f, -90f, timeCount) : 90f;
                    break;
                case 3://up
                    rotateZ = Mathf.Lerp(0f, moveDirection * 90f, timeCount);
                    break;
                case 4://down
                    rotateZ = Mathf.Lerp(0f, -moveDirection * 90f, timeCount);
                    break;
                default:
                    break;
            }
            transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        }
        if (timeCount > prepTime)
        {
            transform.position = newPos;
            if (moveDirection > 0)
            {
                dir = 2;//right
            }
            else
            {
                dir = 1;//left
            }
            timeCount = 0;
            beginPrep = false;
            beginMove = true;
            r.velocity = Vector2.zero;
        }
        if (beginMove)
        {
            r.velocity = new Vector2(moveDirection * speed, 0);
            beginMove = false;
            GameObject skillPreb = Resources.Load(GameInfo.EffectPrefabResDir + muzzlePrefabPath) as GameObject;
            GameObject projectileParticle = Instantiate(skillPreb, transform.position, transform.rotation) as GameObject;
            Destroy(projectileParticle, 3.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Ground" || hit.gameObject.tag == "Platform")
        {
            r = GetComponent<Rigidbody2D>();
            r.velocity = new Vector2(moveDirection * speed, 0);
        }
    }
}
