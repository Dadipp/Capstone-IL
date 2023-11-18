using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongEnemyMove : MonoBehaviour
{

    private Animator anim;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if(transform.position == positions[index])
        {
            if(index == positions.Length -1)
            {
                index = 0;
            }
            else
            {
                flip();
                index++;
            }
        }
        
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
