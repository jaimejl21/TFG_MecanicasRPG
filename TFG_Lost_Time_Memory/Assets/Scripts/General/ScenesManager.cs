using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToNodesMap()
    {
        int nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
        if(nNodesMaps == 6)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            SceneManager.LoadScene("NodesMap" + nNodesMaps);
        }
        
    }
}
