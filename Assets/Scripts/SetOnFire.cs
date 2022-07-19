using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    public GameObject fireModel;
    public bool onFire = false;
    public bool permFire = false;
    public float timer = 8f;
    public GameObject flameDoor;

    float timeLeft;

    public bool isWet = false;
    public bool isRespawning = false;

    //Find Manager
    public Manager manager;
    private SoundManager soundManager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fireModel != null && other.gameObject.layer != 4)
        {
            if (other.gameObject.GetComponent<SetOnFire>() != null)
            {
                if (other.gameObject.GetComponent<SetOnFire>().onFire)
                {
                    fireModel.SetActive(true);
                    onFire = true;
                    soundManager.Play2D("FireLit");
                    if (!permFire)
                        StartCoroutine(TempFire());
                    else
                        soundManager.Play("FireCrack", this.gameObject.transform.position);
                    if (flameDoor != null)
                        flameDoor.GetComponent<FireWall>().CheckFlame();
                }
            }
        }

        if (other.gameObject.layer == 4)
        {
            isWet = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            isWet = false;
        }
    }

    IEnumerator TempFire()
    {
        timeLeft = timer;

        while (timeLeft >= 0)
        {
            if (isWet)
                break;
            timeLeft -= Time.deltaTime;
            //Debug.Log(timeLeft);
            yield return null;
        }

        if (gameObject.tag == "Player" && !isWet)
        {
            isRespawning = true;
            manager.KillPlayer();
        }


        fireModel.SetActive(false);
        onFire = false;
    }

}
