using System.Collections;
using System.Collections.Generic;

public static class GameInfo
{
    public static string FuDrawFileName = "customFuDraw.jpg";
    public static string FuListFileName = "FuList";
    public static string PersistentResDir = "PersistentData/";
    public static string EffectPrefabResDir = "Prefabs/Effects/";

    //Player===============================
    public static int PlayerMoveDirection = 1;

    //--skill 
    public static float PlayerRlsSkillCD = 3.0f;
    public static bool BlockPlayerSkillRls = false;
    public static int PlayerCurrentSkillNum = 0;//what key is input? j 1 /k 2 /l 3

    public static int PlayerSkillAnimeLayer = 1;

    public static int PlayerTotalCost = 9;

    //Fu==========================================
    public static List<Fu> fuList;//static table for all fu
    public static List<FuItem> battleFuList;// the list during battle


    //Monster/Enemy========================================
}

