using UnityEngine;
using System.Collections;

public class FinishPoint : MonoBehaviour
{
    public GameObject videoScene;
    public float videoDuration;
    public GameObject sound;


    public void PlayVideo()
    {
        sound.SetActive(false);
        videoScene.SetActive(true);
        StartCoroutine(DestroyVideo());
    }

    private IEnumerator DestroyVideo()
    {
        yield return new WaitForSeconds(videoDuration);
        Destroy(videoScene);
        SceneController.instance.NextLevel();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayVideo();
        }
    }
}
