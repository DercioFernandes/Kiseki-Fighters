using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSpawner : MonoBehaviour
{
    //public GameObject charPrefab;

    public CharacterDatabase characterDB;
    private int selectedOption = 0;
    private int secondSelectedOption = 0;
    public GameObject uiBarPrefab;
    //public Transform uiCanvas; 
    public Transform uiCanvas1;
    public Transform uiCanvas2;

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public int maxHealth = 100;
    public int maxStam = 20;
    public float spawnOffset = 50f;
    public Vector3 healthBarRotation = new Vector3(0f, 0f, 180f);
    public GameObject magicAttack;
    public GameObject sword;


    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        if(!PlayerPrefs.HasKey("secondSelectedOption"))
        {
            secondSelectedOption = 0;
        }
        Load();
        GameObject char1 = UpdateCharacter(selectedOption);
        GameObject spawnedChar1 = SpawnPrefab(char1, spawnPosition, spawnRotation, uiCanvas1, false);
        
        Vector3 secondSpawnPosition = new Vector3(spawnPosition.x + 1300f, spawnPosition.y, spawnPosition.z);
        GameObject char2 = UpdateCharacter(secondSelectedOption);
        PlayerInput playerInput = char2.GetComponent<PlayerInput>();
        playerInput.defaultActionMap = "ARROW";
        GameObject spawnedChar2 = SpawnPrefab(char2, secondSpawnPosition, Quaternion.Euler(0f, 180f, 0f), uiCanvas2, true);

        //After both are spawned

        PlayerController playerController1 = spawnedChar1.GetComponent<PlayerController>();
        PlayerController playerController2 = spawnedChar2.GetComponent<PlayerController>();

        PlayerHumanSet playerHumanSet1 = spawnedChar1.GetComponent<PlayerHumanSet>();
        PlayerHumanSet playerHumanSet2 = spawnedChar2.GetComponent<PlayerHumanSet>();

        PlayerElfSet playerElfSet1 = spawnedChar1.GetComponent<PlayerElfSet>();
        PlayerElfSet playerElfSet2 = spawnedChar2.GetComponent<PlayerElfSet>();

        PlayerMovement playerMovement1 = spawnedChar1.GetComponent<PlayerMovement>();
        PlayerMovement playerMovement2 = spawnedChar2.GetComponent<PlayerMovement>();

        playerMovement1.playerController = playerController1;
        playerMovement2.playerController = playerController2;
    	
        playerHumanSet1.playerController = playerController1;
        playerHumanSet2.playerController = playerController2;
        playerHumanSet1.enemyHealthBar = playerController2;
        playerHumanSet2.enemyHealthBar = playerController1;
        playerHumanSet1.sword = sword;
        playerHumanSet2.sword = sword;

        playerElfSet1.playerController = playerController1;
        playerElfSet2.playerController = playerController2;
        playerElfSet1.enemyHealthBar = playerController2;
        playerElfSet2.enemyHealthBar = playerController1;
        playerElfSet1.magicAttack = magicAttack;
        playerElfSet2.magicAttack = magicAttack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject SpawnPrefab(GameObject character, Vector3 position, Quaternion rotation, Transform parentCanvas, bool isSecond)
    {
        if (character != null)
        {
            GameObject instantiatedCharacter = Instantiate(character, position, rotation);
            instantiatedCharacter.tag = "Player2";
            SpawnUIBar(instantiatedCharacter, parentCanvas,isSecond);
            return instantiatedCharacter;
        }
        else
        {
            Debug.LogError("character is not assigned.");
            return null;
        }
    }

    void SpawnUIBar(GameObject player, Transform parentCanvas, bool isSecond=false)
    {
        Debug.Log("UI bar spawned for " + player.name);
        GameObject uiBar = Instantiate(uiBarPrefab, parentCanvas);
        uiBar.name = player.name + "UIBar";  // Set unique name for each UI bar

        // Get the PlayerController from the instantiated character
        PlayerController playerController = player.GetComponent<PlayerController>();

        // Find the Health and Stamina sliders in the instantiated UI bar
        HealthBar healthSlider = uiBar.transform.Find("HealthBar").GetComponent<HealthBar>();
        StaminaBar staminaSlider = uiBar.transform.Find("StaminaBar").GetComponent<StaminaBar>();

        healthSlider.name = healthSlider.name + player.name;
        staminaSlider.name = staminaSlider.name + player.name;

        // Link the sliders to the player script
        playerController.healthBar = healthSlider;
        playerController.staminaBar = staminaSlider;
        
        Debug.Log("Assigned HealthBar to " + player.name + ": " + healthSlider.name);
        Debug.Log("Assigned StaminaBar to " + player.name + ": " + staminaSlider.name);

        //print("Type: ");
        //print(playerController.healthBar);
        
        if(isSecond == true){
            print("Is Second");
            RectTransform rectTransformHealth = healthSlider.GetComponent<RectTransform>();
            rectTransformHealth.localPosition += new Vector3(1000f, 0f, 0f);
            rectTransformHealth.localRotation = Quaternion.Euler(healthBarRotation);
            RectTransform rectTransformStamina = staminaSlider.GetComponent<RectTransform>();
            rectTransformStamina.localPosition += new Vector3(1300f, 0f, 0f);
            rectTransformStamina.localRotation = Quaternion.Euler(healthBarRotation);
        }

        playerController.healthBar.SetMaxHealth(maxHealth);
        playerController.staminaBar.SetMaxStamina(maxStam);
    }

    private GameObject UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        GameObject charPrefab = character.characterPrefab;
        return charPrefab;
    }

    private void Load(){
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        secondSelectedOption = PlayerPrefs.GetInt("secondSelectedOption");
    }
}
