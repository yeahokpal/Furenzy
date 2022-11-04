using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SnakeGFX : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
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
