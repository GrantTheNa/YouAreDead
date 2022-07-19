using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEatBody : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerControls player = other.GetComponent<PlayerControls>();

        if (player != null)
            player.BodyEaten(); 
    }
}
