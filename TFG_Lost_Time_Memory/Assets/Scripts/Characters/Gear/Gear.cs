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

        public Info() {}

        public Info(int id, int type, bool equiped)
        {
            this.id = id;
            this.type = type;
            this.equiped = equiped;
        }
    }

    public Info info;
}
