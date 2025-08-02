using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame() {
        Debug.Log("load game");
        // SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame() {
        Debug.Log("quit game");
        Application.Quit();
    }
}
