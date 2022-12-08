using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        aiPath = gameObject.GetComponentInParent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("Frog"))
        {
            if (aiPath.reachedDestination == true)
            {
                animator.Play("frogIdle");
            }
            else
            {
                animator.Play("frogHop");
            }
        }

        if (gameObject.name.Contains("Snake"))
        {
            if (aiPath.reachedDestination == true)
            {
                animator.Play("SnakeIdle");
            }
            else
            {
                animator.Play("SnakeMove");
            }
        }
    }
}
