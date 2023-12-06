using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.CheckpointPosition = transform.position;
            }
        }
    }

    public Vector3 GetCheckpointPosition()
    {
        return transform.position;
    }
}
