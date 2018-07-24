using UnityEngine;
using UnityEditor;

public class FuClassAsset : MonoBehaviour {

    [MenuItem("Assets/Create/FuScriptableObject")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<FuScriptableObject>();
    }
}
