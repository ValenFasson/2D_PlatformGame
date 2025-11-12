using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScoreBoard : MonoBehaviour
{
    public void TryAgain() 
    {
        SceneManager.LoadScene("Bootstrap");
    }
}
