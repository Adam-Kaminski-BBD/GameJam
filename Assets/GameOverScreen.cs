using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public MenuController MenuController;

    public bool gameOver = false;

    public void Setup() {
        MenuController.Disable();
        gameObject.SetActive(true);
        gameOver = true;
        Time.timeScale = 0;
    }

    public void RestartButton() {
        SceneManager.LoadScene("SampleScene");
    }
}
