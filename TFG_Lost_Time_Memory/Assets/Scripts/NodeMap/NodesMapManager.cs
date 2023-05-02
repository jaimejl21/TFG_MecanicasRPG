using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NodesMapManager : MonoBehaviour
{
    public List<GameObject> columnsList;

    public GameObject objAlertPn;

    int actualCol, idGearCount;

    void Start()
    {
        GameManager.inst.GetPlayerPrefs("actualCol", ref actualCol, 0);
        GameManager.inst.GetPlayerPrefs("idGearCount", ref idGearCount, 0);

        //if (PlayerPrefs.HasKey("actualCol"))
        //{
        //    actualCol = PlayerPrefs.GetInt("actualCol");
        //    Debug.Log("key " + actualCol);
        //}
        //else
        //{
        //    actualCol = 0;
        //    Debug.Log("No key");
        //}

        for (int i = 0; i < (actualCol + 1); i++)
        {
            columnsList[i].SetActive(true);
            Debug.Log("column " + i + " active");
        }        
    }

    public void ManageColumns()
    {
        actualCol++;
        PlayerPrefs.SetInt("actualCol", actualCol);
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
                    //Debug.Log("aux " + aux);
                    while(!aux)
                    {
                        //Debug.Log("nodeSelected = " + columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].GetComponent<MapNode>().nodeSelected);
                        if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].GetComponent<MapNode>().nodeSelected == 1)
                        {

                            isActive = true;
                            aux = true;
                        }
                        j++;
                        //Debug.Log("j = " + j + " // prevNodes.Count = " + columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count);
                        if (j >= columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count)
                        {
                            aux = true;
                        }
                        
                    }
                    //Debug.Log("isActive " + isActive);
                    if (!isActive)
                    {
                        columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().SetNodeSelected(0);
                        columnsList[actualCol].transform.GetChild(i).GetComponent<Button>().interactable = false;
                    }
                }
            }
        }      
    }

    public void ObjectAlert()
    {
        objAlertPn.SetActive(true);

        string objNameTxt = objAlertPn.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;



        // int id  int statAmount  int objType  int statType  int rarity  int augment  int stars  bool equiped  int characterId

        Gear.Info gi = new Gear.Info(idGearCount, 50, 0, 2, 2, 0, 0, false, -1);

        idGearCount++;
        PlayerPrefs.SetInt("idGearCount", idGearCount);
        GameManager.inst.idGearCount = idGearCount;
    }
}
