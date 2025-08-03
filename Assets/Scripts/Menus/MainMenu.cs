using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame() {
        Debug.Log("load game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
    }

    public void QuitGame() {
        Debug.Log("quit game");
        Application.Quit();
    }
}
