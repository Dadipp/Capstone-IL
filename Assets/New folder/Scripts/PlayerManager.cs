using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float health = 10; // Darah awal yang akan digunakan jika tidak ada nilai yang disimpan di PlayerPrefs
    
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public float Waktu = 100;
    public static PlayerManager Instance;
    public GameObject portal;
    public GameObject player;
    public static event System.Action OnPlayerDeath;
    public bool GameAktif = true;
    public Text TextTimer;
    public Animator anim;
    public Rigidbody2D rb;
    public static int numberOfCoins;
    public static int highScore;
    public static int numberOfScore;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    private Vector3 checkpointPosition = Vector3.zero;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip checkPointSound;
    [SerializeField] AudioClip obstacleSound;
    public PlayerController playerController;
    public GameObject upssState;
    public Transform floatPoint;
    public Vector3 CheckpointPosition
    {
        get { return checkpointPosition; }
        set { checkpointPosition = value; }
    }

    
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
    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        numberOfScore = PlayerPrefs.GetInt("NumberOfScore", 0);
        spriteRend = GetComponent<SpriteRenderer>();
        UpdateHighScore();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _uiManager = UIManager.Instance;

        // Mengambil nilai darah dari PlayerPrefs. Jika tidak ada, menggunakan nilai awal.
        health = PlayerPrefs.GetFloat("PlayerHealth", 4f);
        health = Mathf.Max(health, 0);
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
                Instantiate(upssState, floatPoint.position, Quaternion.identity, transform);
                StartCoroutine(WaitforFunction());
                //StartCoroutine(Invunerability());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            SoundManager.instance.PlaySound(checkPointSound);
            Checkpoint checkpoint = collision.gameObject.GetComponent<Checkpoint>();
            if (checkpoint != null)
            {
                checkpointPosition = checkpoint.GetCheckpointPosition();
                collision.GetComponent<Collider2D>().enabled = false;
                collision.GetComponent<Animator>().SetTrigger("appear");
            }
        }
    }

    public void AddHealth(float _value)
    {
        health = Mathf.Clamp(health + _value, 0, float.MaxValue);  // Batas bawah diatur ke 0, dan batas atas diatur ke nilai maksimum float
        UIManager.Instance.UpdateHealth(health);
        Debug.Log("Player's health increased. Current health: " + health);
        PlayerPrefs.SetFloat("PlayerHealth", health);
    }

    public void AddCoins(int value)
    {
        numberOfCoins += value;
        coinsText.text = numberOfCoins.ToString();
        CheckHighScore();
        PlayerPrefs.SetInt("NumberOfCoins", numberOfCoins);
        Debug.Log("Player collected coins. Total coins: " + numberOfCoins);
    }

    public void AddScore(int value)
    {
        numberOfScore += value;
        scoreText.text = numberOfScore.ToString();
        CheckHighScore();
        PlayerPrefs.SetInt("NumberOfScore", numberOfScore);
        Debug.Log("Player collected score. Total score: " + numberOfScore);
    }

    void CheckHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (numberOfCoins > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", numberOfCoins);
            UpdateHighScore();
        }
    }


    void UpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("High Score: " + highScore);
        highScoreText.text = $"HighScore: {highScore}";
    }


    private void RespawnPlayer()
    {
        if (portal != null && player != null)
        {
            if (checkpointPosition != Vector3.zero)
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    if (playerController.isTeleporting)
                    {
                        player.transform.position = checkpointPosition;
                        playerController.SetMovePointPosition(transform.position);
                    }
                }          
            }
            else
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
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool CheckDeath()
    {
        SoundManager.instance.PlaySound(deathSound);
        anim.SetTrigger("death");
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

    private IEnumerator WaitforFunction()
    {
        playerController.moveSpeed = 0;
        yield return new WaitForSeconds(2);
        playerController.moveSpeed = 5;
    }

}