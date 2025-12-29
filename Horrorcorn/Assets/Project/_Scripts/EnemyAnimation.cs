using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    private bool walking;

    void Start()
    {
        animator = GetComponent<Animator>();
        EnemySightSensor.PlayerSighted += ChasePlayer;
        EnemyMovement.DestinationReached += Idle;
    }
    
    private void OnDestroy()
    {
        EnemySightSensor.PlayerSighted -= ChasePlayer;
        EnemyMovement.DestinationReached -= Idle;
    }

    private void ChasePlayer()
    {
        if (!walking)
        {
            animator.SetTrigger("ChasePlayer");
            walking = true;
        }
    }

    private void Idle()
    {
        animator.SetTrigger("Idle");
        walking = false;
    }
}
