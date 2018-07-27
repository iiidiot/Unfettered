using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSwordOverScreenBack : MonoBehaviour
{
    public Transform target;
    public string muzzlePrefabPath;
    public float height = 0f;
    public float timeCount = 0;
    public float delay = 0.5f;//how much delay to back it
    public bool hasCalTime = false;
    public Rigidbody2D r;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").transform;
        r = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsInView(transform.position) && !hasCalTime)//确保此函数只触发一次
        {
            hasCalTime = true;
            timeCount = 0;
        }
        if (IsInView(transform.position))
        {
            hasCalTime = false;
            timeCount = 0;
        }
        if (hasCalTime)
        {
            timeCount += Time.deltaTime;
        }
        if(timeCount > delay)
        {
            transform.position = target.position + new Vector3(Random.Range(-5, 5), Random.Range(0, height), 0);
            GameObject muzzleParticle = Resources.Load(GameInfo.EffectPrefabResDir + muzzlePrefabPath) as GameObject;
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 3.0f);
            hasCalTime = false;
            timeCount = 0;
            r.velocity = Vector2.zero;
        }


    }


    public bool IsInView(Vector3 worldPos)
    {

        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;

    }
}
