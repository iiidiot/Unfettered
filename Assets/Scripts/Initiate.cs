using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FuScriptableObject f = Resources.Load<FuScriptableObject>(GameInfo.PersistentResDir + GameInfo.FuListFileName);
        GameInfo.fuList = f.fuList;
        GameInfo.battleFuList = new List<FuItem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
