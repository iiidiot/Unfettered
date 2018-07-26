using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePreparation : MonoBehaviour {
    public Text notion;
    public Transform fuInfo;

    public Text fu1, fu2, fu3;

    public Transform idleFire, runFire;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameInfo.battleFuList.Count <= 0)
        {
            idleFire.gameObject.SetActive(false);
            runFire.gameObject.SetActive(false);
        }
        if (fuInfo.gameObject.activeSelf)
        {
            fu1.text = "";
            fu2.text = "";
            fu3.text = "";
            fuInfo.GetComponent<Text>().text = "Fu Count: " + GameInfo.battleFuList.Count;
            if (GameInfo.battleFuList.Count > 0)
            {
                fu1.text = GameInfo.battleFuList[0].type.ToString();

                if(GameInfo.battleFuList.Count > 1)
                {
                    fu2.text = GameInfo.battleFuList[1].type.ToString();

                    if (GameInfo.battleFuList.Count > 2)
                    {
                        fu3.text = GameInfo.battleFuList[2].type.ToString();
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            notion.text = "Press 'F' to battle preparation";
            battlePrepare();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            notion.text = "";
        }
    }


    void battlePrepare()
    {
        if (Input.GetButtonDown("Interact"))
        {
            GameInfo.battleFuList.Clear();
            for (int i = 0; i < GameInfo.PlayerTotalCost;)
            {
                FuItem ft = new FuItem(i,
                    FuQuality.Good,
                    (FuType)Random.Range(0, (float)FuType.Count));

                GameInfo.battleFuList.Add(ft);

                Fu f = GameInfo.fuList.Find(c => c.type == ft.type);
                i += f.cost;
            }
            fuInfo.gameObject.SetActive(true);
            idleFire.gameObject.SetActive(true);
            runFire.gameObject.SetActive(true);
        }
    }
}
