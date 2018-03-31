using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartClick()
    {
        GameController.currentRound = 0;
        SceneManager.LoadScene("main");
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
