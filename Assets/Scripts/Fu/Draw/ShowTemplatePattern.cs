using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTemplatePattern : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        RawImage r = GetComponent<RawImage>();
        string standard_path = Application.streamingAssetsPath + "/Fu/fireball.jpg";

        Texture2D standard_tx = new Texture2D(Screen.width, Screen.height);
        standard_tx.LoadImage(ImageProcess.getImageByte(standard_path));

        r.texture = standard_tx;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
