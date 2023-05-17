using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesLines : MonoBehaviour
{
    public NodesLines(Info info)
    {
        this.info = info;
    }

    public class Info
    {
        public Vector2 diference;
        public float sign;
        public float heigth;
        public float width;
        public Vector3 linePos;

        public Info() { }

        public Info(Vector2 diference, float sign, float heigth, float width, Vector3 linePos)
        {
            this.diference = diference;
            this.sign = sign;
            this.heigth = heigth;
            this.width = width;
            this.linePos = linePos;
        }
    }

    public Info info;
}
