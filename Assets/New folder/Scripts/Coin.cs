using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1; // Setel nilai ini sesuai dengan jenis koin
    [SerializeField] private AudioClip pickupcoinSound;
    public Animator anim;
    public GameObject coinGope;
    public Transform floatPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupcoinSound);
            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                Instantiate(coinGope, floatPoint.position, Quaternion.identity, transform);
                StartCoroutine(WaitforFunction());
                playerManager.AddCoins(coinValue);
            }
        }
    }

    private IEnumerator WaitforFunction()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

