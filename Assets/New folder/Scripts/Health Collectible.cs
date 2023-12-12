using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(pickupSound);
            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}

