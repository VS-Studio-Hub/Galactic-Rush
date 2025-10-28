using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Disable all Move scripts (stop lane movement)
            Move[] moves = FindObjectsOfType<Move>();
            foreach (Move m in moves)
            {
                m.enabled = false;
                GameManager.gameOver = true;
            }

            // Play hit animation
            if (animator != null)
                animator.Play("Stumble Backwards");

        }
    }
}
