using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
