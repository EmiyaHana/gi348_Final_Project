using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string levelToLoad = "SampleScene"; 

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}