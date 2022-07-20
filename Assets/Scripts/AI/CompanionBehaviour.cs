using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionBehaviour : MonoBehaviour
{

    public PlayerControls playerControls;

    public float startFollowingWithinDist = 3;
    public float stopsFollowingAtDist = 10;

    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerControls.gameObject.transform.position);

        if (playerControls.GetPausedState())
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
            animator.SetBool("isMoving", false);
        }

        //the creature is to far away
        else if (distance > startFollowingWithinDist)
        {
            //tell animator to walk/hop
            if (animator != null)
                animator.SetBool("isMoving", false);
        }

        //if the creature is close
        else if (distance < startFollowingWithinDist)
        {
            GetComponent<NavMeshAgent>().destination = playerControls.gameObject.transform.position + Vector3.right;
            if (animator != null)
                animator.SetBool("isMoving", true);
        }
    }
}
