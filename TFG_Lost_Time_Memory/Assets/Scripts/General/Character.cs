using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Character : MonoBehaviour
{
    public Character(Info info)
    {
        this.info = info;
    }

    [System.Serializable]
    public class Info
    {
        public int id;
        public int pos;
        public bool inTeam;
        public List<Gear.Info> gear;
        public Stats stats;

        public Info() {}

        public Info(int id, int pos, bool inTeam, List<Gear.Info> gear, Stats stats)
        {
            this.id = id;
            this.pos = pos;
            this.inTeam = inTeam;
            this.gear = gear;
            this.stats = stats;
        }
    }

    [System.Serializable]
    public class Stats
    {
        public float baseAtk;
        public float baseDef;
        public float baseHp;
        public float extraAtk;
        public float extraDef;
        public float extraHp;
        public float atk;
        public float def;
        public float hp;

        public Stats(float baseAtk, float baseDef, float baseHp, float extraAtk, float extraDef, float extraHp, float atk, float def, float hp)
        {
            this.baseAtk = baseAtk;
            this.baseDef = baseDef;
            this.baseHp = baseHp;
            this.extraAtk = extraAtk;
            this.extraDef = extraDef;
            this.extraHp = extraHp;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
        }

        public Stats()
        {
            this.baseAtk = 10;
            this.baseDef = 0;
            this.baseHp = 100;
            this.extraAtk = 0;
            this.extraDef = 0;
            this.extraHp = 0;
            this.atk = baseAtk + extraAtk;
            this.def = baseDef + extraDef;
            this.hp = baseHp + extraHp;  
        }
    }

    public Info info;
}
