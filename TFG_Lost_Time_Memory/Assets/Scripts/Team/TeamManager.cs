using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamManager : MonoBehaviour
{
    public GameObject pool;

    [SerializeField]
    GameObject character;

    [SerializeField]
    GameObject[] teamSlots;

    public List<Character.Info> noTeamCharList;
    public List<Character.Info> inTeamCharList;

    void Start()
    {
        noTeamCharList = GameManager.allChar.ToList();
        inTeamCharList = GameManager.myTeam.ToList();

        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.Log(auxTeamList[i]);
        //}

        pool = GameObject.FindGameObjectWithTag("Pool");

        CharInventory();
        TeamSlots();
    }

    void TeamSlots()
    {
        for (int i = 0; i < 6; i++)
        {
            if (inTeamCharList[i].id != -1)
            {
                character.GetComponent<Character>().info = inTeamCharList[i];
                GameObject aux = Instantiate(character, teamSlots[i].transform);
                aux.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                aux.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                teamSlots[i].GetComponent<DropSlot>().item = teamSlots[i].transform.GetChild(0).gameObject;
                teamSlots[i].transform.GetChild(0).GetComponent<DragHandler>().startParent = pool.transform;
            }
        }
    }

    void CharInventory()
    {
        foreach (Character.Info c in noTeamCharList)
        {
            character.GetComponent<Character>().info = c;
            //Debug.Log(character.GetComponent<Character>().info.inTeam);
            if(character.GetComponent<Character>().info.inTeam != true)
            {
                Instantiate(character, pool.transform);
            } 
        } 
    }

    public bool CanAddToTeam()
    {
        int cont = 0;
        for(int i=0; i<6; i++)
        {
            if(teamSlots[i].GetComponent<DropSlot>().item != null)
            {
                cont++;
            }
        }
        Debug.Log("Cont inTeam: " + cont);
        if(cont < 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveTeam()
    {
        for (int i=0; i<6; i++)
        {
            if(teamSlots[i].GetComponent<DropSlot>().item != null)
            {
                int pos = teamSlots[i].GetComponent<DropSlot>().slotPos;
                Character.Info inSlotChar = new Character.Info(teamSlots[i].GetComponent<DropSlot>().item.GetComponent<Character>().info.id, pos, true);
                inTeamCharList.RemoveAt(i);
                inTeamCharList.Insert(i, inSlotChar);

                //bool aux = false;
                //int j = 0;

                //while(!aux)
                //{
                //    if(auxCharList[j].id == inSlotChar.id)
                //    {
                //        auxCharList.Remove(auxCharList[j]);
                //        aux = true;
                //    }
                //    j++;
                //}

                //Debug.Log(auxTeamList[i]);
                //Debug.Log(inSlotChar.inTeam);
            }
            else
            {
                inTeamCharList.RemoveAt(i);
                inTeamCharList.Insert(i, new Character.Info(-1, -1, false));
                //Debug.Log(auxTeamList[i]);
            }
        }

        //foreach(Character.Info c in auxCharList)
        //{
        //    Debug.Log(c.id);
        //}

        GameManager.myTeam = inTeamCharList;
        //GameManager.allChar = noTeamCharList;

        for (int i = 0; i < GameManager.allChar.Count; i++)
        {
            Debug.Log(" Pos " + i + ": " + GameManager.allChar[i].id);
        }

        //Debug.Log("Saved");

        GameManager.inst.SaveListsToJson();

        //GameManager.inst.ShowMyTeam();
    }
}
