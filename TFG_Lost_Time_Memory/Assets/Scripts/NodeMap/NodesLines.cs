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
        public GameObject lineGO;
        public Vector3 linePos;

        public Info(GameObject lineGO, Vector3 linePos)
        {
            this.lineGO = lineGO;
            this.linePos = linePos;
        }
    }

    public Info info;
}
