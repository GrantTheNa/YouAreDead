using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealBody : MonoBehaviour
{
    private PlayerControls playerObject;
    public ParticleSystem healParticle;

    private void Start()
    {
        playerObject = FindObjectOfType<PlayerControls>();
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Rigidbody>();

        if (player != null)
            playerObject.Heal();

        Instantiate(healParticle, other.gameObject.transform);
    }
}
