using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterSO[] availableCharacters;
    public Transform spawnPoint;
    // public CharacterSelectorManager characterSelectorManager;

    public CinemachineCamera virtualCamera;
    void Start()
    {
        // if (characterSelectorManager != null)
        // {
        //     characterSelectorManager.SpawnSelectedCharacter();
        // }
        // else
        // {
        //     Debug.Log("characterSelectorManager does not exist in this scene");
        // }
        SpawnSelectedCharacter();
    }


    public void SpawnSelectedCharacter()
    {
        string characterName = PlayerPrefs.GetString("SelectedCharacter", "");
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError("Karakter seçilmemiş! Lütfen CharacterSelectionScene'de bir karakter seç.");
            return;
        }
        foreach (CharacterSO character in availableCharacters)
        {
            if (character.characterName == characterName)
            {
                GameObject player = Instantiate(character.characterPrefab, spawnPoint.position, Quaternion.identity);
                Player playerScript = player.GetComponent<Player>();
                playerScript.InitializeCharacter(character);
                player.GetComponent<PlayerHealth>().SetMaxHealth(character.health);
                // break;
                if (virtualCamera != null)
                {
                    virtualCamera.Follow = player.transform;
                    virtualCamera.LookAt = player.transform;
                }
                return;
            }
        }


        Debug.LogError("Seçilen karakter bulunamadı! Karakter listesi eksik olabilir.");
    }

}
