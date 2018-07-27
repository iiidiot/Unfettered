using UnityEngine;
using System.Collections;

[System.Serializable]
public class Fu
{
    public FuType type;
    public string tex_path;
    public string prefab_path;
    public Vector2 move_speed;
    public int cost;
    public float cd;//in seconds

    public Fu(FuType _type, string _tex_path, string _prefab_path, Vector2 _ms, int _cost, float _cd)
    {
        type = _type;
        tex_path = _tex_path;
        prefab_path = _prefab_path;
        move_speed = _ms;
        cost = _cost;
        cd = _cd;
    }
    public Fu()
    {
        type = 0;
        tex_path = "";
        prefab_path = "";
        move_speed = Vector2.zero;
        cost = 0;
        cd = 0;
    }
}

public class FuItem
{
    public int id;
    public FuQuality quality;
    public FuType type;

    public FuItem() { }
    public FuItem(int _i, FuQuality _q, FuType _t)
    {
        id = _i;
        quality = _q;
        type = _t;
    }
}

public enum FuQuality
{
    Best,
    Good,
    Mediocre,
    Bad,
    Count
}
public enum FuType
{
    //Fire
    FireBall,

    //Water
    FrostBall,

    //Metal
    MetalSwordSummon,
    MetalSwordMoveH,

    Count
}