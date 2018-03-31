using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public void MenuClick()
    {
        SceneManager.LoadScene("menu");
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
