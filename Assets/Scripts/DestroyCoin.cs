using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoin : MonoBehaviour
{
    public Manager manager;
    public bool isFireCoin;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFireCoin)
        {
            manager.FireCoin();
        }
        else
        {
            manager.SerpentCoin();
        }
        DestorySelf();
    }

    private void DestorySelf()
    {
        Destroy(this.gameObject);
    }
}
