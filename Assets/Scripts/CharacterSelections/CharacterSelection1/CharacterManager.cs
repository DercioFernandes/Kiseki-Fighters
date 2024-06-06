using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Text nameText;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        Load();
        UpdateCharacter(selectedOption);
    }

    public void NextOption(){
        selectedOption++;
        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption(){
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        artworkSprite.enabled = false; 
        artworkSprite.enabled = true; 
        nameText.text = character.characterName;
    }

    private void Load(){
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save(){
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        PlayerPrefs.Save();
    }

    public void ChangeScene(int sceneID){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
