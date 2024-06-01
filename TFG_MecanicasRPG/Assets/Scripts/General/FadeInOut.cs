using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Animator animator;

    string sceneName;

    public void FadeToScene(string scene)
    {
        sceneName = scene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneName);
    }
}
