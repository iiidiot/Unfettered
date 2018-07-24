using UnityEngine;
using System.Collections;

[System.Serializable]
public class Fu
{
    public FuType type;
    public string tex_path;
    public string prefab_path;
    public float move_speed;

    public Fu(FuType _type, string _tex_path, string _prefab_path, float _ms)
    {
        type = _type;
        tex_path = _tex_path;
        prefab_path = _prefab_path;
        move_speed = _ms;
    }
    public Fu()
    {
        type = 0;
        tex_path = "";
        prefab_path = "";
        move_speed = 0;
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

    Count
}