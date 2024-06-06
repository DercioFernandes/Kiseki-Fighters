using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSpawner : MonoBehaviour
{
    public CharacterDatabase characterDB;
    private int selectedOption;
    private int secondSelectedOption;
    public GameObject uiBarPrefab;
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
    public PlayerInputManager playerInputManager;
    public GameObject char1;
    public GameObject char2;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            print("There is no SelectedOption");
            selectedOption = 0;
        }
        else
        {
            selectedOption = PlayerPrefs.GetInt("selectedOption");
        }
        if (!PlayerPrefs.HasKey("secondSelectedOption"))
        {
            print("There is no SecondSelectedOption");
            secondSelectedOption = 0;
        }
        else
        {
            secondSelectedOption = PlayerPrefs.GetInt("secondSelectedOption");
        }
        Load();

        char1 = UpdateCharacter(selectedOption);
        char2 = UpdateCharacter(secondSelectedOption);

        if (char1 == null || char2 == null)
        {
            Debug.LogError("Character prefab is not found.");
            return;
        }

        if (Gamepad.all.Count < 2)
        {
            Debug.LogError("Not enough gamepads connected!");
            return;
        }

        // Ensure PlayerInputManager has the player prefab set
        playerInputManager.playerPrefab = char1;
        GameObject spawnedChar1 = JoinPlayer(0, Gamepad.all[1], false);

        playerInputManager.playerPrefab = char2; // Switch to second character prefab
        GameObject spawnedChar2 = JoinPlayer(1, Gamepad.all[0], true);

        // Assign controllers and components
        AssignControllers(spawnedChar1, spawnedChar2);
    }

    GameObject JoinPlayer(int playerIndex, InputDevice device, bool isSecond)
    {
        if (playerInputManager == null)
        {
            Debug.LogError("PlayerInputManager is not assigned!");
            return null;
        }

        if (isSecond)
        {
            spawnPosition = new Vector3(spawnPosition.x + 1300f, spawnPosition.y, spawnPosition.z);
            spawnRotation = Quaternion.Euler(0f, 180f, 0f);
        }

        var playerInput = playerInputManager.JoinPlayer(playerIndex, -1, "VIAArcade", device);

        if (playerInput == null)
        {
            Debug.LogError("Failed to join player with index: " + playerIndex);
            return null;
        }

        // Set the custom position and rotation
        playerInput.transform.position = spawnPosition;
        playerInput.transform.rotation = spawnRotation;

        // Customize the spawned character
        SetupCharacter(playerInput.gameObject, playerIndex, isSecond);
        return playerInput.gameObject;
    }

    void SetupCharacter(GameObject character, int playerIndex, bool isSecond)
    {
        if (character == null)
        {
            Debug.LogError("Character is null in SetupCharacter.");
            return;
        }

        PlayerController playerController = character.GetComponent<PlayerController>();

        if (isSecond)
        {
            playerController.tag = "Player2";
        }
        else
        {
            playerController.tag = "Player1";
        }

        // Setup UI bars
        SpawnUIBar(character, isSecond ? uiCanvas2 : uiCanvas1, isSecond);
    }

    void SpawnUIBar(GameObject player, Transform parentCanvas, bool isSecond = false)
    {
        GameObject uiBar = Instantiate(uiBarPrefab, parentCanvas);
        uiBar.name = player.name + "UIBar";

        PlayerController playerController = player.GetComponent<PlayerController>();
        HealthBar healthSlider = uiBar.transform.Find("HealthBar").GetComponent<HealthBar>();
        StaminaBar staminaSlider = uiBar.transform.Find("StaminaBar").GetComponent<StaminaBar>();

        healthSlider.name = healthSlider.name + player.name;
        staminaSlider.name = staminaSlider.name + player.name;

        playerController.healthBar = healthSlider;
        playerController.staminaBar = staminaSlider;

        if (isSecond)
        {
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
        if (character == null)
        {
            Debug.LogError("Character not found in database for selected option: " + selectedOption);
            return null;
        }
        return character.characterPrefab;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        secondSelectedOption = PlayerPrefs.GetInt("secondSelectedOption");
    }

    private void AssignControllers(GameObject player1, GameObject player2)
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogError("One or both player objects are null.");
            return;
        }

        PlayerController playerController1 = player1.GetComponent<PlayerController>();
        PlayerController playerController2 = player2.GetComponent<PlayerController>();

        PlayerHumanSet playerHumanSet1 = player1.GetComponent<PlayerHumanSet>();
        PlayerHumanSet playerHumanSet2 = player2.GetComponent<PlayerHumanSet>();

        PlayerElfSet playerElfSet1 = player1.GetComponent<PlayerElfSet>();
        PlayerElfSet playerElfSet2 = player2.GetComponent<PlayerElfSet>();

        PlayerMovement playerMovement1 = player1.GetComponent<PlayerMovement>();
        PlayerMovement playerMovement2 = player2.GetComponent<PlayerMovement>();

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
}
