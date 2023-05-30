using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class MapNode : MonoBehaviour
{
    public int id, type, nodeSelected;
    public bool selected;

    string toSceneName;

    TextMeshProUGUI btnTMP;
    Button btn;
    public List<MapNode> nextNodes, prevNodes;
    public GameObject parentPanel;
    public NodesMapManager nmm;

    void Start()
    {
        nmm = FindObjectOfType<NodesMapManager>();
        
        btnTMP = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        parentPanel = gameObject.transform.parent.gameObject;
        btn = gameObject.GetComponent<Button>();

        if (PlayerPrefs.HasKey("nodeSelected" + id))
        {
            nodeSelected = PlayerPrefs.GetInt("nodeSelected" + id);
            //Debug.Log("Has key  nodeSelected " + id + ": " + nodeSelected);
        }
        else
        {
            nodeSelected = -1;
            SetNodeSelected(-1);
            //Debug.Log("Has no key  nodeSelected " + id + ": " + nodeSelected);
        }
        //nmm.AddItemToNodesPrefsList(id.ToString());
        SetTypeName();
        SetButton();
    }

    public void SelectNode()
    {
        selected = true;
        SetNodeSelected(1);
        SetTypeTxt();
        ColorBlock cb = btn.colors;
        cb.disabledColor = new Color(0, 1, 0, 1f);
        btn.colors = cb;
        for(int i = 0; i < parentPanel.transform.childCount; i++)
        {
            parentPanel.transform.GetChild(i).GetComponent<Button>().interactable = false;
            if(parentPanel.transform.GetChild(i).GetComponent<MapNode>().selected != true)
            {
                parentPanel.transform.GetChild(i).GetComponent<MapNode>().SetNodeSelected(0);
            }
        }
        nmm.SaveSrPosX();
        nmm.ManageColumns(type);
    }

    public void SetNodeSelected(int mode)
    {
        nodeSelected = mode;
        PlayerPrefs.SetInt("nodeSelected" + id, nodeSelected);
        //Debug.Log("nodeSelected " + id + ": " + nodeSelected);
    }

    void SetButton()
    {
        switch(nodeSelected)
        {
            case -1:
                break;
            case 0:
                gameObject.GetComponent<Button>().interactable = false;
                break;
            case 1:
                SetTypeTxt();
                ColorBlock cb = btn.colors;
                cb.disabledColor = new Color(0, 1, 0, 1f);
                btn.colors = cb;
                gameObject.GetComponent<Button>().interactable = false;
                break;
            default:
                break;
        }
    }

    void SetTypeName()
    {
        switch (type)
        {
            case 0:
                toSceneName = "Fight";
                break;
            case 1:
                toSceneName = "Merchant";
                break;
            case 2:
                toSceneName = "Blacksmith";
                break;
            case 3:
                toSceneName = "Team";
                break;
            case 4:
                toSceneName = "Dialogue";
                break;
            case 5:
                toSceneName = "Characters";
                break;
            case 6:
                toSceneName = "Item";
                break;
            case 7:
                toSceneName = "Tavern";
                break;
            default:
                break;
        }
    }

    void SetTypeTxt()
    {
        switch(type)
        {
            case 0:
                btnTMP.text = "Fight";
                break;
            case 1:
                btnTMP.text = "Merchant";
                break;
            case 2:
                btnTMP.text = "Blacksmith";
                break;
            case 3:
                btnTMP.text = "Team";
                break;
            case 4:
                btnTMP.text = "Dialogue";
                break;
            case 5:
                btnTMP.text = "Characters";
                break;
            case 6:
                btnTMP.text = "Item";;
                break;
            case 7:
                btnTMP.text = "Tavern";
                break;
            default:
                break;
        }
    }

    public void ChangeToDialogueScene(string conver)
    {
        SceneManager.LoadScene(toSceneName);
        GameManager.inst.converName = conver;
    }

    public void ChangeToFightScene(int enemyTeam)
    {
        SceneManager.LoadScene(toSceneName);
        GameManager.inst.enemyTeam = enemyTeam;
    }

    public void ChangeToTavernScene(int nextNodeMap)
    {
        SceneManager.LoadScene(toSceneName);
        PlayerPrefs.SetInt("nNodesMaps", nextNodeMap);
    }

    public void ChangeToScene()
    {
        SceneManager.LoadScene(toSceneName);
    }
}
