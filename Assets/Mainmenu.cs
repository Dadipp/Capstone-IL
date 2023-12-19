using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    public static bool howToPlay = false;
    public GameObject HowToPlayUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (howToPlay)
            {
                Back();
            }
            else
            {
                Pause();
            }
        }
    }

    void Back()
    {
        HowToPlayUI.SetActive(false);
        howToPlay = false;
    }

    void Pause()
    {
        HowToPlayUI.SetActive(true);
        howToPlay = true;
    }
}
