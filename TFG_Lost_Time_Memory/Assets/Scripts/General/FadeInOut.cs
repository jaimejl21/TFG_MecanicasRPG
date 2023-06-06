using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Animator animator;

    int sceneToLoad;

    public void FadeToScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        //GameManager.inst.converName = dialogue;
        SceneManager.LoadScene(sceneToLoad);
    }
}
