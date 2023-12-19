using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject upssObs;
    public Transform floatPoint;
    [SerializeField] private AudioClip obtaclesSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SoundManager.instance.PlaySound(obtaclesSound);
            Instantiate(upssObs, floatPoint.position, Quaternion.identity, transform);
        }
    }
}
