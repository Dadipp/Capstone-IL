using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private Animator anim;

    [SerializeField] Transform[] Points;

    [SerializeField] private float moveSpeed;

    private int pointsIndex;
    private int flipIndex;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        // Set position of Enemy as position of the first waypoint
        transform.position = Points[pointsIndex].transform.position;
        flipIndex = Points.Length - 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsIndex <= Points.Length -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed * Time.deltaTime);

            if(transform.position == Points[pointsIndex].transform.position)
            {
                pointsIndex += 1;
                
                if (pointsIndex == flipIndex)
                {
                    Flip();
                }
                if (pointsIndex == Points.Length)
                {
                    pointsIndex = 0;
                    Flip();
                }
            }

        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
