using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpoint;
    public Transform floatPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                Instantiate(checkpoint, floatPoint.position, Quaternion.identity, transform);
                playerManager.CheckpointPosition = transform.position;
            }
        }
    }

    public Vector3 GetCheckpointPosition()
    {
        return transform.position;
    }
}
