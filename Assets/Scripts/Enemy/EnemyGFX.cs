/*
 * Programmer: Caden
 * Purpose: Make enemies play their south idle if they are not seeking a player
 * Input: If enemy is tracking
 * Output: Correct enemy animation
 */

using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    #region Variables
    public Animator animator;
    public AIPath aiPath;

    #endregion

    #region Default Methods
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
            if (aiPath.hasPath == true)
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
            if (aiPath.hasPath == true)
            {
                animator.Play("SnakeIdle");
            }
            else
            {
                animator.Play("SnakeMove");
            }
        }

        if (gameObject.name.Contains("Bat"))
        {
            if (aiPath.reachedDestination == true)
            {
                animator.Play("BatIdle");
            }
            else
            {
                animator.Play("BatMove");
            }
        }
    }
    #endregion
}
