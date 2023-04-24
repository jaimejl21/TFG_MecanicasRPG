using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (actualCol < columnsList.Count)
        {
            columnsList[actualCol].SetActive(true);
            //Debug.Log("actualCol " + actualCol);
            for (int i = 0; i < columnsList[actualCol].transform.childCount; i++)
            {
                //Debug.Log("prevNodes.Count " + columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count);
                if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count > 0)
                {
                    int j = 0;
                    bool aux = false;
                    bool isActive = false;
                    Debug.Log("aux " + aux);
                    while(!aux)
                    {
                        //Debug.Log("nodeSelected = " + columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].GetComponent<MapNode>().nodeSelected);
                        if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].GetComponent<MapNode>().nodeSelected == 1)
                        {

                            isActive = true;
                            aux = true;
                        }
                        j++;
                        Debug.Log("j = " + j + " // prevNodes.Count = " + columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count);
                        if (j >= columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count)
                        {
                            aux = true;
                        }
                        
                    }
                    Debug.Log("isActive " + isActive);
                    if (!isActive)
                    {
                        columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().SetNodeSelected(0);
                        columnsList[actualCol].transform.GetChild(i).GetComponent<Button>().interactable = false;
                    }
                }
            }
        }      
    }
}
