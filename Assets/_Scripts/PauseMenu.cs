using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;        // pause menu UI
	public GameObject pauseOverlay;		// pause menu shadow overlay
	public GameObject pauseButton;      // pause button UI
	
	void Start()
	{
		pauseMenu.SetActive(false);  // pause menu starts disabled
	}
	
	// displays pause menu
	public void PauseClick()
	{
		Pause();
	}
	
	// returns to game
	public void ResumeClick()
	{
		Unpause();
	}

    // restarts level
    public void RestartClick()
    {
        GameController.currentRound = 0;
        SceneManager.LoadScene("main");
		Unpause();
    }

    // loads main menu
    public void MenuClick()
	{
        SceneManager.LoadScene("menu");
		Invoke("Unpause", .5f);
	}

	private void Pause()
	{
		GameController.isPaused = true;
		Time.timeScale = 0;     // freezes time
		pauseMenu.SetActive(true);
		pauseOverlay.SetActive(true);
		pauseButton.SetActive(false);
		AudioSource source = GameController.GetInstance().GetComponent<AudioSource>();
		source.Pause();
	}

	private void Unpause()
	{
		pauseMenu.SetActive(false);
		pauseOverlay.SetActive(false);
		pauseButton.SetActive(true);
		AudioSource source = GameController.GetInstance().GetComponent<AudioSource>();
		source.UnPause();
		GameController.isPaused = false;
		Time.timeScale = 1;     // restarts time
	}
}