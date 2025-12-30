using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    private bool walking;
    
    private EnemyMovement movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<EnemyMovement>();
    }
    

    public void ChasePlayer()
    {
        if (!walking)
        {
            animator.SetTrigger("ChasePlayer");
            walking = true;
        }
    }

    public void IdleWalk()
    {
        animator.SetTrigger("IdleWalk");
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
        walking = false;
    }
}
