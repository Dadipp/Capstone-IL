using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float health = 10; // Darah awal yang akan digunakan jika tidak ada nilai yang disimpan di PlayerPrefs
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
            UIManager.Instance.UpdateHealth(health);
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

        // Mengambil nilai darah dari PlayerPrefs. Jika tidak ada, menggunakan nilai awal.
        health = PlayerPrefs.GetFloat("PlayerHealth");

        _uiManager.UpdateHealth(health);

        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.playerManager = this;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Health--;
            if (CheckDeath())
            {
                RespawnPlayer();
            }
            if (health <= 0)
            {
                health = 0;
                Debug.Log("You're dead");
                OnPlayerDeath?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    private void RespawnPlayer()
    {
        if (portal != null && player != null)
        {
            player.transform.position = portal.transform.position;

            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (playerController.isTeleporting)
                {
                    playerController.SetMovePointPosition(portal.transform.position);
                }
            }
        }
    }

    private bool CheckDeath()
    {
        return Health >= 1;
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
                Waktu--;
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