using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DestroySound : MonoBehaviour
{
    private void Awake()
    {
        AudioSource audio = GetComponent<AudioSource>();
        float timer = audio.clip.length;
        StartCoroutine(DestroySelf(timer));
    }

    IEnumerator DestroySelf(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }

}
