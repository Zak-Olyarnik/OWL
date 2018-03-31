using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	// Singleton
	public static GameController instance;
	public static GameController GetInstance() { return instance; }
	void Awake() { instance = this; }

	//public enum PowerUp { ScatterShot, TrailShot };

	// object references set in editor
	public GameObject asteroid;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject victoryMenu;        // victory menu UI
	public GameObject defeatMenu;        // defeat menu UI
	public GameObject pauseButton;      // pause button UI

	public static int currentRound;
	public static bool isPaused = false;
	public int currentEnemies;

	private PlayerController pc;	// reference to player object
    private float playerHealth;
	private float asteroidSpeed = 3.0f;
	private float offscreenSpawnX = 10.5f;
	private float offscreenSpawnY = 6.5f;
	private int enemiesPerWaveInRound = 15;
	private bool isRoundOver = false;


	void Start()
	{
		pc = PlayerController.GetInstance();
		startRound();	// does round reset maintenance
	}

	void Update()
	{
		// check endgame conditions
		if (currentEnemies == 0)
		{
			endRound();
		}
		else if (pc.GetHealth() <= 0.0f)
		{
			SceneManager.LoadScene("defeat");
		}
	}

	void endRound()
	{
        isRoundOver = true;
		//if (currentWave == 2)
        //{
			SceneManager.LoadScene("victory");
        //}
        //else
        //{
        //  //Go to upgrade panel
        //}
	}

	//void applyUpgrade()
	//{
    //    pc.ApplyUpgrade("chosen upgrade");
    //}

	void startRound()
	{
        Invoke("asteroidSpawn", 6.0f);
        isRoundOver = false;
		isPaused = false;
        pc.HealthInit();
        currentRound += 1;
        currentEnemies = currentRound * 3 * enemiesPerWaveInRound;
 
        for (int i = 0; i < currentRound * 3; i++)
        {
            Invoke("enemySpawn", i * 20.0f);
        }

	}

    private void enemySpawn()
    {
        for (int i = 0; i < enemiesPerWaveInRound; i++)
        {
            float xPos, yPos;
            int randXY = Random.Range(0, 2);
            int randDir = Random.Range(0, 2);

            // fixed x, random y spawn
            if (randXY < 1)
            {
                if (randDir < 1)
                {
                    xPos = -offscreenSpawnX;
                }
                else
                {
                    xPos = offscreenSpawnX;
                }

                yPos = Random.Range(-offscreenSpawnY, offscreenSpawnY);
            }
            else
            {
                if (randDir < 1)
                {
                    yPos = -offscreenSpawnY;
                }
                else
                {
                    yPos = offscreenSpawnY;
                }
                xPos = Random.Range(-offscreenSpawnX, offscreenSpawnX);
            }
 
			// instantiate diverse enemies
            if (i < enemiesPerWaveInRound * 0.6f)
                Instantiate(enemy1, new Vector3(xPos, yPos, 0), new Quaternion(0, 0, 0, 0));
			else if (i < enemiesPerWaveInRound * 0.8f)
                Instantiate(enemy2, new Vector3(xPos, yPos, 0), new Quaternion(0, 0, 0, 0));
            else
                Instantiate(enemy3, new Vector3(xPos, yPos, 0), new Quaternion(0, 0, 0, 0));
        }

    }

    private void asteroidSpawn() {
		float xPos, yPos;
		int randXY = Random.Range(0, 2);
		int randDir = Random.Range(0, 2);
		Vector3 movementDir;

		// fixed x, random y spawn
		if (randXY < 1)	{
			if (randDir < 1) {
				xPos = -offscreenSpawnX; 
				movementDir = transform.right;
			}
			else {
				xPos = offscreenSpawnX;
				movementDir = -1 * transform.right;
			}

			yPos = Random.Range(-offscreenSpawnY, offscreenSpawnY);
		}
		else {
			if (randDir < 1) {
				yPos = -offscreenSpawnY;
				movementDir = transform.up;
			}
			else {
				yPos = offscreenSpawnY;
                movementDir = -1 * transform.up;
			}
			xPos = Random.Range(-offscreenSpawnX, offscreenSpawnX);
		}
		GameObject a = Instantiate(asteroid, new Vector3(xPos, yPos, 0), new Quaternion(0, 0, 0, 0));
		a.GetComponent<Rigidbody2D>().velocity = movementDir * asteroidSpeed;
        
		if (!isRoundOver)	// repeat spawn while still playing
        {
            Invoke("asteroidSpawn", 6.0f);
        }
	}
}
