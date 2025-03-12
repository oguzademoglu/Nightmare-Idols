using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectorManager : MonoBehaviour
{
    public CharacterSO[] availableCharacters;
    CharacterSO selectedCharacter;
    public Transform spawnPoint;
    public GameObject characterButtonPrefab;
    public Transform buttonParent;

    public Button playButton;

    GameObject previewCharacter;

    void Start()
    {
        playButton.interactable = false;
        GenerateCharacterButtons();
    }

    private void GenerateCharacterButtons()
    {
        foreach (CharacterSO character in availableCharacters)
        {
            GameObject buttonObj = Instantiate(characterButtonPrefab, buttonParent);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = character.characterName;

            button.onClick.AddListener(() => SelectCharacter(character));
        }
    }

    public void SelectCharacter(CharacterSO character)
    {
        if (previewCharacter != null) Destroy(previewCharacter);
        selectedCharacter = character;
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter.characterName);
        PlayerPrefs.Save();
        previewCharacter = Instantiate(selectedCharacter.characterPrefab, spawnPoint.position, Quaternion.identity);
        playButton.interactable = true;
        // SceneManager.LoadScene("GameScene");
    }

    // public void SpawnSelectedCharacter()
    // {
    //     string characterName = PlayerPrefs.GetString("SelectedCharacter", "");
    //     foreach (CharacterSO character in availableCharacters)
    //     {
    //         if (character.characterName == characterName)
    //         {
    //             GameObject player = Instantiate(character.characterPrefab, spawnPoint.position, Quaternion.identity);
    //             Player playerScript = player.GetComponent<Player>();
    //             playerScript.InitializeCharacter(character);
    //             player.GetComponent<PlayerHealth>().SetMaxHealth(character.health);
    //             break;
    //         }
    //     }
    // }

    public void StartGame()
    {
        if (selectedCharacter != null) SceneManager.LoadScene("GameScene");
    }
}
