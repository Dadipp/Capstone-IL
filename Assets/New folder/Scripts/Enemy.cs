using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerManager _playerManager;
    public Transform destination;
    private GameObject player;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RespawnPlayerAtDestination()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                player.transform.position = destination.transform.position;
            }
        }
    }

    private void MOvement()
    {
        // Implement your enemy movement logic here
    }
}
