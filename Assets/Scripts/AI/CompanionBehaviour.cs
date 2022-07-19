using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionBehaviour : MonoBehaviour
{
    public float startFollowingWithinDist = 10;
    public float stopsFollowingAtDist = 50;

    GameObject player;
    Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            Debug.Log("Player isnt tagged correctly");

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        //the creature is to far away
        if (distance > startFollowingWithinDist)
        {          
            //tell animator to walk/hop
            if (animator != null)
                animator.SetBool("isMoving", true);
        }

        //if the creature is close
        else if (distance < startFollowingWithinDist)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position + Vector3.right;
            if (animator != null)
                animator.SetBool("isMoving", false);
        }
    }
}
