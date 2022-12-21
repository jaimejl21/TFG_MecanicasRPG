using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear : MonoBehaviour
{
    public Gear(Info info)
    {
        this.info = info;
    }

    [System.Serializable]
    public class Info
    {
        public int id;
        public int type;
        public bool equiped;
        public int characterId;

        public Info() {}

        public Info(int id, int type, bool equiped, int characterId)
        {
            this.id = id;
            this.type = type;
            this.equiped = equiped;
            this.characterId = characterId;
        }
    }

    public Info info;
}
