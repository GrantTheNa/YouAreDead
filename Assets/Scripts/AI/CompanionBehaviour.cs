using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionBehaviour : MonoBehaviour
{
    public float startFollowingWithinDist = 3;
    public float stopsFollowingAtDist = 10;

    GameObject player;
    Animator animator;
    PlayerControls playerControls;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            Debug.Log("Player isnt tagged correctly");

        animator = GetComponentInChildren<Animator>();

        playerControls = player.GetComponent<PlayerControls>();


    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (playerControls.GetPausedState())
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
            animator.SetBool("isMoving", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        //the creature is to far away
        if (distance > startFollowingWithinDist)
        {
            //tell animator to walk/hop
            if (animator != null)
                animator.SetBool("isMoving", false);
        }

        //if the creature is close
        else if (distance < startFollowingWithinDist)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position + Vector3.right;
            if (animator != null)
                animator.SetBool("isMoving", true);
        }
    }
}
