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


    private void OnTriggerEnter(Collider other)
    {
        if (fireModel != null)
        {
            if (other.gameObject.GetComponent<SetOnFire>() != null)
            {
                if (other.gameObject.GetComponent<SetOnFire>().onFire)
                {
                    fireModel.SetActive(true);
                    onFire = true;
                    if (!permFire)
                        StartCoroutine(TempFire());
                    if (flameDoor != null)
                        flameDoor.GetComponent<FireWall>().CheckFlame();
                }
            }
        }

    }

    IEnumerator TempFire()
    {
        yield return new WaitForSeconds(timer);
        fireModel.SetActive(false);
        onFire = false;
    }

}
