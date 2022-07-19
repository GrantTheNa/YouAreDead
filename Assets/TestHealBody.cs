using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealBody : MonoBehaviour
{
    public PlayerControls playerObject;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Rigidbody>();

        if (player != null)
            playerObject.Heal();
    }
}
