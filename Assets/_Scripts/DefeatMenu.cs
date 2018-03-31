using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour {

    public void MenuClick()
    {
        SceneManager.LoadScene("menu");
    }

    public void RetryClick()
    {
        GameController.currentRound = 0;
        SceneManager.LoadScene("main");
    }

	public void QuitClick()
	{
		Application.Quit();
	}
}
