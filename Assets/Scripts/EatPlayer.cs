using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPlayer : MonoBehaviour
{
    public PlayerControls playerControls;

    private void Start()
    {
        playerControls = FindObjectOfType<PlayerControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerControls.Eaten();
    }
}
