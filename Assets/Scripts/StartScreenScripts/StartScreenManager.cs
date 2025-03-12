using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelectionScene");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is closed");
    }
}
