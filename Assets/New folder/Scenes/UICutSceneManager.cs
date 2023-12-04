using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICutSceneManager : MonoBehaviour
{
    [SerializeField] private float health = 10;
    public float Waktu = 100;
    [SerializeField] private UIManager _uiManager;
    public static PlayerManager Instance;
    public GameObject portal;
    public GameObject player;
    public static event System.Action OnPlayerDeath;
    public bool GameAktif = true;
    public Text TextTimer;
    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public float Health
    {
        get { return health; }

        set
        {
            health = value;
            _uiManager.UpdateHealth(health);  // Perubahan ini
            PlayerPrefs.SetFloat("PlayerHealth", health);
        }
    }

    private void Awake()
    {
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
    }

    void Start()
    {
        _uiManager = UIManager.Instance;

        health = PlayerPrefs.GetFloat("PlayerHealth");

        _uiManager.UpdateHealth(health);
    }

    void SetText()
    {
        int Menit = Mathf.FloorToInt(Waktu / 60);
        int Detik = Mathf.FloorToInt(Waktu % 60);
        TextTimer.text = Menit.ToString("00") + ":" + Detik.ToString("00");
    }

    float s;
    void Update()
    {
        coinsText.text = numberOfCoins.ToString();
        if (GameAktif)
        {
            s += Time.deltaTime;
            if (s >= 1)
            {
                s = 0;
            }
        }

        if (GameAktif && Waktu <= 0)
        {
            Debug.Log("GameKalah");
            GameAktif = false;
            OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
        }

        SetText();
    }
}
