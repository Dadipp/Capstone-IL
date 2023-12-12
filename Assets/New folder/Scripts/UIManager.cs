using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI healthText;
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealth(float value)
    {
        int intValue = Mathf.RoundToInt(value);
        healthText.text = intValue.ToString();
    }


    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= EnableGameOverMenu;
    }
    void Start()
    {
        
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
