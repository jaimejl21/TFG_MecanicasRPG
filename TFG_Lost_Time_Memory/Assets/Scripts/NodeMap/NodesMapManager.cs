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

    public GameObject objAlertPn;
    public ScrollRect sr;
    public GameObject lineGO, linesParent;

    int actualCol, idGearCount;

    public List<NodesLines.Info> nodesLinesList;
    //List<int> nextNodesColIds;

    void Start()
    {
        GameManager.inst.GetPlayerPrefs("actualCol", ref actualCol, 0);
        GameManager.inst.GetPlayerPrefs("idGearCount", ref idGearCount, 0);

        nodesLinesList = GameManager.nodesLinesList.ToList();

        if (actualCol >= columnsList.Count) actualCol = columnsList.Count - 1;

        for (int j = 0; j <= actualCol; j++)
        {
            columnsList[j].SetActive(true);
        }
        sr.horizontalNormalizedPosition = 0;

        if(nodesLinesList.Count > 0)
        {
            
            foreach (NodesLines.Info nli in nodesLinesList)
            {
                GameObject line = Instantiate(lineGO, linesParent.transform);

                float angle = Vector2.Angle(Vector2.right, nli.diference) * nli.sign; ;
                line.transform.Rotate(0, 0, angle);
                line.GetComponent<RectTransform>().sizeDelta = new Vector2(nli.width, nli.heigth);
                line.transform.position = nli.linePos;               
            }
        }     
    }

    public void DrawAllLines()
    {
        int nodesCount;
        for (int j = 0; j <= actualCol; j++)
        {
            nodesCount = columnsList[j].transform.childCount;
            for (int i = 0; i < nodesCount; i++)
            {
                if (columnsList[j].transform.GetChild(i).GetComponent<MapNode>().nextNodes.Count > 0)
                {
                    for (int n = 0; n < columnsList[j].transform.GetChild(i).GetComponent<MapNode>().nextNodes.Count; n++)
                    {
                        GameObject a = columnsList[j].transform.GetChild(i).gameObject;
                        GameObject b = columnsList[j].transform.GetChild(i).GetComponent<MapNode>().nextNodes[n].gameObject;
                        DrawLine(a, b);
                        //b.GetComponent<MapNode>().SetColId();
                        //nextNodesColIds.Add(b.GetComponent<MapNode>().colId);
                        //Debug.Log("Draw line from node " + a.GetComponent<MapNode>().id + " to " + b.GetComponent<MapNode>().id);
                    }
                }
            }
        }
        GameManager.nodesLinesList = nodesLinesList;
    }

    //void SetActualCol()
    //{
    //    actualCol++;
    //    auxActualCol = actualCol;
    //    int aux = actualCol;
    //    foreach (int i in nextNodesColIds)
    //    {
    //        if (i > aux)
    //        {
    //            aux = i;
    //        }
    //    }
    //    nextNodesColIds.Clear();
    //    actualCol = aux;
    //    PlayerPrefs.SetInt("actualCol", actualCol);
    //}

    public void ManageColumns()
    {
        DrawAllLines();
        //SetActualCol();
        actualCol++;
        PlayerPrefs.SetInt("actualCol", actualCol);
        int nodesCount;
        if (actualCol < columnsList.Count)
        {
            if (columnsList[actualCol] != null) columnsList[actualCol].SetActive(true);
            //if (linesGroupList[actualCol - 1] != null) linesGroupList[actualCol - 1].SetActive(true);
            nodesCount = columnsList[actualCol].transform.childCount;
            for (int i = 0; i < nodesCount; i++)
            {
                if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes.Count > 0)
                {
                    int j = 0;
                    bool aux = false;
                    bool isActive = false;
                    while (!aux)
                    {
                        if (columnsList[actualCol].transform.GetChild(i).GetComponent<MapNode>().prevNodes[j].GetComponent<MapNode>().nodeSelected == 1)
                        {

                            isActive = true;
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
                        if (nodesCount == 1 || i == (nodesCount - 1))
                        {
                            ManageColumns();
                        }
                    }
                }
            }
        }
    }

    public void ObjectAlert()
    {
        objAlertPn.SetActive(true);
        TextMeshProUGUI objNameTMP = objAlertPn.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        int obj = new Random().Next(0, 4);
        int rarity = new Random().Next(0, 3);

        switch (obj)
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

                objNameTMP.color = SetGearStatColor(statType);
                objNameTMP.text = "" + SetRarityName(rarity) + " " + SetName(objType);
                break;
            case 1:
                int upMats = new Random().Next(5, 11);
                GameManager.inst.upMats += upMats;
                PlayerPrefs.SetInt("upMats", GameManager.inst.upMats);

                objNameTMP.color = SetGearStatColor(-1);
                objNameTMP.text = "" + upMats + " Upgrade materials";
                break;
            case 2:
                int awMats = new Random().Next(1, 4);
                GameManager.inst.awMats += awMats;
                PlayerPrefs.SetInt("awMats", GameManager.inst.awMats);

                objNameTMP.color = SetGearStatColor(-1);
                objNameTMP.text = "" + awMats + " Awake materials";
                break;
            case 3:
                int lvlUpMats = new Random().Next(5, 16);
                if(rarity == 0)
                {
                    GameManager.inst.lvlUpMatC += lvlUpMats;
                    PlayerPrefs.SetInt("amountC", GameManager.inst.lvlUpMatC);
                }
                else if(rarity == 1)
                {
                    GameManager.inst.lvlUpMatR += lvlUpMats;
                    PlayerPrefs.SetInt("amountR", GameManager.inst.lvlUpMatR);
                }
                else
                {
                    GameManager.inst.lvlUpMatSR += lvlUpMats;
                    PlayerPrefs.SetInt("amountSR", GameManager.inst.lvlUpMatSR);
                }

                objNameTMP.color = SetGearStatColor(-1);
                objNameTMP.text = "" + lvlUpMats + " " + SetRarityName(rarity) + " Level Up materials";
                break;
        }
        GameManager.inst.objectAlert = false;
    }

    public void DrawLine(GameObject objA, GameObject objB)
    {
        /*spawn a prefab image "lineConnection" as angleBar*/
        GameObject angleBar = Instantiate(lineGO, objB.transform.position, Quaternion.identity);
        NodesLines.Info nli = new NodesLines.Info();
        /**/
        /*calculate angle*/
        Vector2 diference = objA.transform.position - objB.transform.position;
        nli.diference = diference;
        float sign = (objA.transform.position.y < objB.transform.position.y) ? -1.0f : 1.0f;
        nli.sign = sign;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;
        angleBar.transform.Rotate(0, 0, angle);
        /**/
        /*calculate length of bar*/
        float height = 10;
        float width = Vector2.Distance(objB.transform.position, objA.transform.position);
        nli.heigth = height;
        nli.width = width;
        angleBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        /**/
        /*calculate midpoint position*/
        float newposX = objB.transform.position.x + (objA.transform.position.x - objB.transform.position.x) / 2;
        float newposY = objB.transform.position.y + (objA.transform.position.y - objB.transform.position.y) / 2;
        angleBar.transform.position = new Vector3(newposX, newposY, 0);
        nli.linePos = angleBar.transform.position;
        /***/
        /*set parent to objB*/
        angleBar.transform.SetParent(linesParent.transform, true);

        nodesLinesList.Add(nli);
    }

    string SetName(int objType)
    {
        switch (objType)
        {
            case 0:
                return "Bracer";
            case 1:
                return "Neck";
            case 2:
                return "Belt";
            case 3:
                return "Ring";
            case 4:
                return "Earring";
            case 5:
                return "Orb";
            case 6:
                return "Sword";
            case 7:
                return "Spear";
            case 8:
                return "Scythe";
            case 9:
                return "Dagger";
            case 10:
                return "Staff";
            case 11:
                return "Bow";
            case 12:
                return "Axe";
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
