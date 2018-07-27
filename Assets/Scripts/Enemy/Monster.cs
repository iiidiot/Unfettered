using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster{

    public float health;
    public int level;
    public MonsterType type;
    public string prefab_path;

    public Monster(float h, int l, MonsterType t, string p)
    {
        health = h;
        level = l;
        type = t;
        prefab_path = p;
    }
    public Monster()
    {
        health = 0;
        level = 0;
        type = 0;
        prefab_path = "";
    }
}

public enum MonsterType
{
    Monster1,
    Count
}
