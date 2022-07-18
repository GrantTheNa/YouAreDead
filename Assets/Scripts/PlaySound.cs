using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public string name = "Enter Name";
    public bool UI_sound = false;
    public Transform transform = null;

    //
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void SoundPlay()
    {
        if (UI_sound)
            soundManager.Play2D(name);
        else
            soundManager.Play(name, transform.position);


    }


}
