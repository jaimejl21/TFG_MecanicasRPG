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
        public int statAmount;
        public int objType;
        public int statType;
        public int rarity;
        public bool equiped;
        public int characterId;

        public Info() {}

        public Info(int id, int statAmount, int objType, int statType, int rarity, bool equiped, int characterId)
        {
            this.id = id;
            this.statAmount = statAmount;
            this.objType = objType;
            this.statType = statType;
            this.rarity = rarity;
            this.equiped = equiped;
            this.characterId = characterId;
        }
    }

    public Info info;
}
