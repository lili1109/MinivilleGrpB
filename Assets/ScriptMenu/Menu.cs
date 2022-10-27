using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Destroy(GameObject.FindWithTag("MenuTheme"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



    public void GoOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void GoRules()
    {
        SceneManager.LoadScene("Rules");
    }

    public void GoCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
