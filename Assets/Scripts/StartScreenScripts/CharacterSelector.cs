using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public CharacterSO[] availableCharacters;
    CharacterSO selectedCharacter;
    public Transform spawnPoint;

    public void SelectCharacter(int characterIndex)
    {
        selectedCharacter = availableCharacters[characterIndex];
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter.characterName);
        SceneManager.LoadScene("GameScene");
    }

    public void SpawnSelectedCharacter()
    {
        string characterName = PlayerPrefs.GetString("SelectedCharacter", "");
        foreach (CharacterSO character in availableCharacters)
        {
            if (character.characterName == characterName)
            {
                GameObject player = Instantiate(character.characterPrefab, spawnPoint.position, Quaternion.identity);
                Player playerScript = player.GetComponent<Player>();
                playerScript.InitializeCharacter(character);
                player.GetComponent<PlayerHealth>().SetMaxHealth(character.health);
                break;
            }
        }
    }
}
