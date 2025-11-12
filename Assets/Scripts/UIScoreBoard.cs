using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScoreBoard : MonoBehaviour
{
    private SceneStackManager managerDeRun;

    void Start()
    {
        managerDeRun = FindObjectOfType<SceneStackManager>();
    }

    public void TryAgain()
    {
        if (managerDeRun != null)
        {
            managerDeRun.resetRun();
        }

        SceneManager.LoadScene("Bootstrap"); 
    }
}
