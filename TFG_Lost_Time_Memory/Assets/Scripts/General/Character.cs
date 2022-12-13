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
        
        public Info() {}

        public Info(int id, int pos, bool inTeam)
        {
            this.id = id;
            this.pos = pos;
            this.inTeam = inTeam;
        }
    }

    public Info info;
}
