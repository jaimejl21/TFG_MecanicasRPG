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
        public float atk;
        public float dfs;
        public float crt;
        public float hp;

        public Stats(float atk, float dfs, float crt, float hp)
        {
            this.atk = atk;
            this.dfs = dfs;
            this.crt = crt;
            this.hp = hp;
        }

        public Stats()
        {
            this.atk = 10;
            this.dfs = 0;
            this.crt = 0;
            this.hp = 100;
        }
    }

    public Info info;
}
