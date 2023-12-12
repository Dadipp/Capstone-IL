using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public float Score;
    public float highscore;
    public Text scoretext;
    public Text highscoretext;


    public void AddScore()
    {
        Score++;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = Score.ToString();
    }
}
