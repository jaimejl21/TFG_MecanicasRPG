using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using System.Linq;

public class NodesMapManager : MonoBehaviour
{
    public List<GameObject> columnsList, linesGroupList;
    //public List<int> nodesPrefsList;

    public GameObject objAlertPn;
    public ScrollRect sr;
    public GameObject lineGO, linesParent;

    public float srPosX;
    public int actualCol, idGearCount, firstNodeId, lastNodeId, death;

    void Start()
    {
        GameManager.inst.GetIntPlayerPrefs("actualCol", ref actualCol, 0);
        GameManager.inst.GetIntPlayerPrefs("idGearCount", ref idGearCount, 0);
        GameManager.inst.GetIntPlayerPrefs("death", ref death, 0);
        GameManager.inst.GetFloatPlayerPrefs("srPosX", ref srPosX, 0);


        if (death == 1)
        {
            for(int i = firstNodeId; i <= lastNodeId; i++)
            {
                if (PlayerPrefs.HasKey("nodeSelected" + i))
                {
                    PlayerPrefs.DeleteKey("nodeSelected" + i);
                }
            }
            death = 0;
            PlayerPrefs.SetInt("death", death);
        }

        for (int j = 0; j <= actualCol; j++)
        {
            columnsList[j].SetActive(true);
        }
        Debug.Log("restartSr: " + GameManager.inst.restartSr);
        if(GameManager.inst.restartSr)
        {

            GameManager.inst.restartSr = false; 
            sr.horizontalNormalizedPosition = srPosX;
        }
        else
        {
            sr.horizontalNormalizedPosition = 0f;
        }
        
        if (actualCol > 0) DrawAllLines();

        if(GameManager.inst.objectAlert)
        {
            ObjectAlert();
        }
    }

    public void DrawAllLines()
    {
        foreach (Transform child in linesParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int nodesCount;
        for (int j = 0; j <= actualCol; j++)
        {
            nodesCount = columnsList[j].transform.childCount;
            for (int i = 0; i < nodesCount; i++)
            {
                if (columnsList[j].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count > 0)
                {
                    for (int n = 0; n < columnsList[j].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count; n++)
                    {
                        GameObject a = columnsList[j].transform.GetChild(i).gameObject;
                        GameObject b = columnsList[j].transform.GetChild(i).GetComponent<MapNode>().prevNodes[n].gameObject;
                        DrawLine(a, b);
                        //Debug.Log("Draw line from node " + a.GetComponent<MapNode>().id + " to " + b.GetComponent<MapNode>().id);
                        //Debug.Log("Column " + j + " node " + i + " prev node " + n);
                    }
                }
            }
        }
    }

    public void DrawNextLines()
    {
        int nodesCount = columnsList[actualCol].transform.childCount;
        for (int i = 0; i < nodesCount; i++)
        {
            if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count > 0)
            {
                for (int n = 0; n < columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count; n++)
                {
                    GameObject a = columnsList[actualCol].transform.GetChild(i).gameObject;
                    GameObject b = columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[n].gameObject;
                    //Debug.Log("Before: draw line from pos a " + a.transform.position + " to pos b " + b.transform.position);
                    DrawLine(a, b);
                    //Debug.Log("Draw line from node " + a.GetComponent<MapNode>().id + " to " + b.GetComponent<MapNode>().id);
                    //Debug.Log("Column " + actualCol + " node " + i + " prev node " + n);
                    //Debug.Log("After: draw line from pos a " + a.transform.position + " to pos b " + b.transform.position);
                }
            }
        }
    }

    public void SaveSrPosX()
    {
        srPosX = sr.horizontalNormalizedPosition;
        PlayerPrefs.SetFloat("srPosX", srPosX);
    }

    public void ManageColumns(int type)
    {
        actualCol++;
        PlayerPrefs.SetInt("actualCol", actualCol);
        int nodesCount;
        if (actualCol < columnsList.Count)
        {
            if (type == 6)
            {
                if (columnsList[actualCol] != null) columnsList[actualCol].SetActive(true);
                DrawNextLines();
            }
            nodesCount = columnsList[actualCol].transform.childCount;
            bool anyNodeActive = false;
            for (int i = 0; i < nodesCount; i++)
            {
                if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count > 0)
                {
                    int j = 0;
                    bool aux = false;
                    bool isActive = false;
                    while (!aux)
                    {
                        if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].
                            GetComponent<MapNode>().nodeSelected == 1)
                        {

                            isActive = true;
                            anyNodeActive = true;
                            aux = true;
                        }
                        j++;
                        if (j >= columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count)
                        {
                            aux = true;
                        }
                    }
                    if (!isActive)
                    {
                        columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().SetNodeSelected(0);
                        columnsList[actualCol].transform.GetChild(i).GetComponent<Button>().interactable = false;
                    }
                }
            }
            //Debug.Log("anyNodeActive: " + anyNodeActive);
            if(!anyNodeActive)
            {
                ManageColumns(type);
            }
        }
    }

    public void ObjectAlert()
    {
        objAlertPn.SetActive(true);
        ObjectAlert oa = objAlertPn.transform.GetChild(0).GetComponent<ObjectAlert>();
        List<int> objToGet = new List<int> { 0, 1, 2, 3 };
        int amount = new Random().Next(1, 4);
        oa.ActivateTexts(amount+1);

        for(int i = 0; i <= amount; i++)
        {
            int pos = new Random().Next(0, objToGet.Count);
            int rarity = new Random().Next(0, 3);

            switch (objToGet[pos])
            {
                case 0:
                    // int id  int statAmount  int objType  int statType  int rarity  int augment  int stars  bool equiped  int characterId
                    int objType = new Random().Next(0, 13);
                    int statType;
                    if (objType > 5)
                    {
                        statType = -1;
                    }
                    else
                    {
                        statType = new Random().Next(0, 3);
                    }

                    Gear.Info gi = new Gear.Info(idGearCount, GameManager.inst.AuxSetStatAmount(objType, rarity), objType, statType, rarity, 0, 0, false, -1);

                    idGearCount++;
                    PlayerPrefs.SetInt("idGearCount", idGearCount);
                    GameManager.inst.idGearCount = idGearCount;
                    GameManager.allGear.Add(gi);
                    GameManager.inst.SaveListsToJson();

                    oa.objTxts[i].color = SetGearStatColor(statType);
                    oa.objTxts[i].text = "" + SetRarityName(rarity) + " " + SetName(objType);
                    break;
                case 1:
                    int upMats = new Random().Next(14, 25);
                    GameManager.inst.upMats += upMats;
                    PlayerPrefs.SetInt("upMats", GameManager.inst.upMats);

                    oa.objTxts[i].color = SetGearStatColor(-1);
                    oa.objTxts[i].text = "" + upMats + " materiales de mejora";
                    break;
                case 2:
                    int awMats = new Random().Next(1, 4);
                    GameManager.inst.awMats += awMats;
                    PlayerPrefs.SetInt("awMats", GameManager.inst.awMats);

                    oa.objTxts[i].color = SetGearStatColor(-1);
                    oa.objTxts[i].text = "" + awMats + " materiales de despertar";
                    break;
                case 3:
                    int lvlUpMats = new Random().Next(5, 16);
                    if (rarity == 0)
                    {
                        GameManager.inst.lvlUpMatC += lvlUpMats;
                        PlayerPrefs.SetInt("amountC", GameManager.inst.lvlUpMatC);
                    }
                    else if (rarity == 1)
                    {
                        GameManager.inst.lvlUpMatR += lvlUpMats;
                        PlayerPrefs.SetInt("amountR", GameManager.inst.lvlUpMatR);
                    }
                    else
                    {
                        GameManager.inst.lvlUpMatSR += lvlUpMats;
                        PlayerPrefs.SetInt("amountSR", GameManager.inst.lvlUpMatSR);
                    }

                    oa.objTxts[i].color = SetGearStatColor(-1);
                    oa.objTxts[i].text = "" + lvlUpMats + " " + SetRarityName(rarity) + " materiales de subir nivel";
                    break;
            }
            objToGet.RemoveAll(item => item == objToGet[pos]);
        }

        
        int coins = new Random().Next(700, 2001);
        GameManager.inst.coins += coins;
        PlayerPrefs.SetInt("coins", GameManager.inst.coins);
        oa.objTxts[amount].color = SetGearStatColor(-1);
        oa.objTxts[amount].text = "" + coins + " monedas";

        GameManager.inst.objectAlert = false;
    }

    public void DrawLine(GameObject objA, GameObject objB)
    {
        GameObject angleBar = Instantiate(lineGO, objB.transform.position, Quaternion.identity);

        Vector2 diference = objA.transform.position - objB.transform.position;
        float sign = (objA.transform.position.y < objB.transform.position.y) ? -1.0f : 1.0f;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;
        angleBar.transform.Rotate(0, 0, angle);

        float height = 10;
        float width = Vector2.Distance(objB.transform.position, objA.transform.position);
        angleBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        float newposX = objB.transform.position.x + (objA.transform.position.x - objB.transform.position.x) / 2;
        float newposY = objB.transform.position.y + (objA.transform.position.y - objB.transform.position.y) / 2;
        angleBar.transform.position = new Vector3(newposX, newposY, 0);
        //Debug.Log("antes: " + angleBar.transform.position);

        angleBar.transform.SetParent(linesParent.transform, true);
        //Debug.Log("despues: " + angleBar.transform.position);
    }

    //public void AddItemToNodesPrefsList(string id)
    //{
    //    bool prefIsAdded = false;
    //    foreach (string item in nodesPrefsList)
    //    {
    //        if (item.Contains("nodeSelected" + id))
    //        {
    //            prefIsAdded = true;
    //        }
    //    }
    //    if (!prefIsAdded) nodesPrefsList.Add("nodeSelected" + id);
    //    GameManager.nodesPrefsList = nodesPrefsList;
    //}

    string SetName(int objType)
    {
        switch (objType)
        {
            case 0:
                return "Brazalete";
            case 1:
                return "Collar";
            case 2:
                return "Cinturón";
            case 3:
                return "Anillo";
            case 4:
                return "Pendientes";
            case 5:
                return "Orbe";
            case 6:
                return "Espada";
            case 7:
                return "Lanza";
            case 8:
                return "Guadaña";
            case 9:
                return "Daga";
            case 10:
                return "Bastón";
            case 11:
                return "Arco";
            case 12:
                return "Hacha";
            default:
                return "";
        }
    }

    Color SetGearStatColor(int statType)
    {
        switch (statType)
        {
            case -1:
                return Color.black;
            case 0:
                return new Color(.5f, 0f, 0f, 1f);
            case 1:
                return new Color(0f, 0f, .5f, 1f);
            case 2:
                return new Color(0f, .5f, 0f, 1f);
            default:
                return Color.black;
        }
    }

    string SetRarityName(int rarity)
    {
        switch (rarity)
        {
            case 0:
                return "C";
            case 1:
                return "R";
            case 2:
                return "SR";
            default:
                return "";
        }
    }
}
