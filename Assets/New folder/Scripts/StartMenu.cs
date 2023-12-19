using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject videoScene;
    public float videoDuration;
    public GameObject sound;

    private IEnumerator DestroyVideo()
    {
        yield return new WaitForSeconds(videoDuration);
        Destroy(videoScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        sound.SetActive(false);
        videoScene.SetActive(true);
        StartCoroutine(DestroyVideo());
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
