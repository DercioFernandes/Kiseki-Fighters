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
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void EndGame(string characterName)
    {
        string message = characterName + " was defeated.";
        nameText.text = message;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f; // Pause the game
        StartCoroutine(ReturnToMainMenuAfterDelay(5f)); 
    }

    IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSecondsRealtime(delay);

        // Call the ReturnToMainMenu method
        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}
