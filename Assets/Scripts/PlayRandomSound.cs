using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        float number = Random.Range(8f, 25f);

        yield return new WaitForSeconds(number);

        PlaySound();

    }


    private void PlaySound()
    {
        int randomSound = Random.Range(1, 5);

        switch (randomSound)
        {
            case 1:
                soundManager.Play2D("Crow_1");
                break;
            case 2:
                soundManager.Play2D("Crow_2");
                break;
            case 3:
                soundManager.Play2D("Owl");
                break;
            case 4:
                soundManager.Play2D("Raven");
                break;
            case 5:
                soundManager.Play2D("Thunder_1");
                break;
            case 6:
                soundManager.Play2D("Thunder_2");
                break;
        }

        StartCoroutine(Wait());
    }
}
