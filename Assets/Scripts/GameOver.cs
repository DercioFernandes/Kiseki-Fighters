using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{

    public Canvas gameOverCanvas;
    public TextMeshProUGUI  nameText;
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void EndGame(string characterName)
    {
        string message = characterName + " was defeated.";
        nameText.text = message;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f; 
        StartCoroutine(ReturnToMainMenuAfterDelay(5f)); 
    }

    IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
}
