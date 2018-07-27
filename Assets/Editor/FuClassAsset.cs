using UnityEngine;
using UnityEditor;

public class FuClassAsset : MonoBehaviour {

    [MenuItem("Assets/Create/FuScriptableObject")]
    public static void CreateFuAsset()
    {
        ScriptableObjectUtility.CreateAsset<FuScriptableObject>();
    }

    [MenuItem("Assets/Create/MonsterScriptableObject")]
    public static void CreateMonsterAsset()
    {
        ScriptableObjectUtility.CreateAsset<MonsterScriptableObject>();
    }
}
