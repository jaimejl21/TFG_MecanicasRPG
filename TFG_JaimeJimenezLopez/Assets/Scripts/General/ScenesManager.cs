using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    FadeInOut fio;

    private void Start()
    {
        fio = FindObjectOfType<FadeInOut>();
    }

    public void ChangeScene(string sceneName)
    {
        if (fio != null)
        {
            fio.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ReturnToNodesMap(bool objAlert)
    {
        if (GameManager.inst.mode == GameManager.modeEnum.Game)
        {
            if (objAlert)
            {
                GameManager.inst.objectAlert = true;
            }
            int nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
            Debug.Log("nNodesMaps: " + nNodesMaps);
            if (nNodesMaps == 6)
            {
                if (fio != null)
                {
                    fio.FadeToScene("Lobby");
                }
                else
                {
                    SceneManager.LoadScene("Lobby");
                }
            }
            else
            {
                if (fio != null)
                {
                    fio.FadeToScene("NodesMap" + nNodesMaps);
                }
                else
                {
                    SceneManager.LoadScene("NodesMap" + nNodesMaps);
                }
            }
        }
        else
        {
            //if (objAlert)
            //{
            //    GameManager.inst.objectAlert = true;
            //}
            //int nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
            //Debug.Log("nNodesMaps: " + nNodesMaps);
            //if (nNodesMaps == 6)
            //{
            //    if (fio != null)
            //    {
            //        fio.FadeToScene("Lobby");
            //    }
            //    else
            //    {
            //        SceneManager.LoadScene("Lobby");
            //    }
            //}
            //else
            //{
            //    if (fio != null)
            //    {
            //        fio.FadeToScene("NodesMap" + nNodesMaps);
            //    }
            //    else
            //    {
            //        SceneManager.LoadScene("NodesMap" + nNodesMaps);
            //    }
            //}
            if (fio != null)
            {
                fio.FadeToScene("Lobby");
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
        }   
    }
    public void ChangeToFightScene(int enemyTeam)
    {
        GameManager.inst.enemyTeam = enemyTeam;
        if (fio != null)
        {
            fio.FadeToScene("Fight");
        }
        else
        {
            SceneManager.LoadScene("Fight");
        }
    }

    public void ChangeToDialogueScene(string conver)
    {
        GameManager.inst.converName = conver;
        if (fio != null)
        {
            fio.FadeToScene("Dialogue");
        }
        else
        {
            SceneManager.LoadScene("Dialogue");
        }
    }

    public void ChangeToPrologueScene(string conver)
    {
        GameManager.inst.converName = conver;
        if (fio != null)
        {
            fio.FadeToScene("PrologueDialogue");
        }
        else
        {
            SceneManager.LoadScene("PrologueDialogue");
        }
    }
}
