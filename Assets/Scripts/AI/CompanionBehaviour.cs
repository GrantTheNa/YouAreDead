using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionBehaviour : MonoBehaviour
{
    public float interestDist = 10;
    public float maxInterest = 50;

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
        
        //if the creature isnt near the player but in interest distance them
        if (distance > interestDist && distance < maxInterest)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position + Vector3.right;

            //tell animator to walk/hop
            if (animator != null)          
                animator.SetBool("isMoving", true);        
        }

        //if the creature is at the player then idle
        else if (distance < interestDist)
        {
            //tell animator to idle
            if (animator != null)         
                animator.SetBool("isMoving", false);
           
            //create a small chance of it doing something funny
        }
    }
}
