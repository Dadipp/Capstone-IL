using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Rigidbody2D rb;
    public LayerMask whatStopsMovement;
    public Animator anim;
    public bool isTeleporting = false;
    Vector3 movement;
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        EnableMovement();
        rb = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
        anim = GetComponent<Animator>();
    }

    private void Update()
    { 
        Movement();
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += DisableMovement;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= DisableMovement;
    }
    private void Movement()
    {
        // Tambahkan pengecekan pemain hidup di sini
        if (playerManager.Health > 0 && playerManager.Waktu > 0000)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }
                }
            }
            else
            {

            }
        }
    }

    public void SetMovePointPosition(Vector3 newPosition)
    {
        movePoint.position = newPosition;
    }

    private void DisableMovement()
    {
        anim.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void EnableMovement()
    {
        anim.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
