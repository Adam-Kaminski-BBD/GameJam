using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timerLength = 5.0f; // Length of the timer in seconds
    private float currentTime = 0.0f;
    private bool isTimerRunning = false;
    public Text timeText;
    public GameObject targetPrefab;

    public MenuController menuController;
    public GameOverScreen gameOverScreen;

    private void Start()
    {
        StartTimer();
        currentTime = timerLength;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            DisplayTime(currentTime);
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                isTimerRunning = false;

                Player_Replay effectScript = targetPrefab.GetComponent<Player_Replay>();
                if (effectScript != null)
                {
                    // There should be a pause and another countdown so that the player can understand whats going on.
                    DestroyBullets();
                    effectScript.TriggerEffect();
                    ResetTimer();
                    if (!gameOverScreen.gameOver)
                    {
                        StartTimer();
                    }
                }


            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        Debug.Log("Timer reset");
        menuController.ShowMenu();
    }

    public void ResetTimer()
    {
        //TODO
        //Destroy all elements with the "Bullet Tag"
        currentTime = timerLength;
        isTimerRunning = false;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeText.text = string.Format("{0}: {1:00}","LOOP IN", timeToDisplay);
    }

    void DestroyBullets()
    {
        GameObject[] prefabsBullet = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] prefabsWalls = GameObject.FindGameObjectsWithTag("Wall_Placed");
        GameObject[] prefabsSlime = GameObject.FindGameObjectsWithTag("Slime");

        foreach(GameObject prefab in prefabsBullet)
        {
            Destroy(prefab);
        }
        foreach (GameObject prefab in prefabsWalls)
        {
            Destroy(prefab);
        }
        foreach (GameObject prefab in prefabsSlime)
        {
            Destroy(prefab);
        }
    }
}


