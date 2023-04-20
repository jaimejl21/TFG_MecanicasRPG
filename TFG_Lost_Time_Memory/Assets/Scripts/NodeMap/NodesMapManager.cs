using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesMapManager : MonoBehaviour
{
    public List<GameObject> columnsList;

    int actualCol;

    void Start()
    {
        actualCol = 0;
    }

    public void ManageColumns()
    {
        actualCol++;
        columnsList[actualCol].SetActive(true);
    }

}
