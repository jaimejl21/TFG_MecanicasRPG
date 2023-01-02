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
        public int objType;
        public int statType;
        public bool equiped;
        public int characterId;

        public Info() {}

        public Info(int id, int objType, int statType, bool equiped, int characterId)
        {
            this.id = id;
            this.objType = objType;
            this.statType = statType;
            this.equiped = equiped;
            this.characterId = characterId;
        }
    }

    public Info info;
}
