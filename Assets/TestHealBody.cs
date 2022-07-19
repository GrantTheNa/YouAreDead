using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealBody : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerControls player = other.GetComponent<PlayerControls>();

        if (player != null)
            player.Heal();
    }
}
