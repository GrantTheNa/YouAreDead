using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    /////////Set-Up Tutorial for the Main Method///////
    //// Paste the following to the variables in the script you want to play sounds in.
    // private SoundManager soundManager;

    //// Then on Void Start() or Void Awake() put this:
    // soundManager = FindObjectOfType<SoundManager>();

    //To call sounds, use the following lines:
    //3D Sounds ///
    // soundManager.Play("Sound Name", object.transform.position);
    //2D Sounds ///
    // soundManager.Play2D("Sound Name");

    ////////Alternative Method: less-recommended, use if only needed!///////
    ////For some situations, you can call the code itself by simply finding it (should be used for only 1-time uses objects, if the same line is repeated, use the other method):
    //3D Sounds ///
    // FindObjectOfType<SoundManager>().Play("Sound Name", this.transform.position);
    //2D Sounds ///
    // FindObjectOfType<SoundManager>().Play2D("Sound Name");


    //TIP! If the sound effect seems to not be playing, check if you named the sound and the code calling for the sound, the exact same, also check the Sound Manager Prefab to see if the scene version you're working with has the sound currently
    public void Play(string name, Vector3 position) //Play 3D sound
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        foreach (Sound s in sounds)
        {
            if (s.name == name) //break;
            {
                s.source = soundGameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                float varient = Random.Range(1, s.pitchVarient);
                s.source.pitch = s.pitch * varient;
                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = 1f;
                s.source.maxDistance = 4f;
                s.source.loop = s.loop;
                s.source.Play();
                if (!s.source.loop)
                    soundGameObject.AddComponent<DestroySound>();

            }

        }
    }

    public void Play2D(string name) //Play 2D sound
    {
        GameObject soundGameObject = new GameObject("Sound");
        foreach (Sound s in sounds)
        {
            if (s.name == name) //break;
            {
                s.source = soundGameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                float varient = Random.Range(1, s.pitchVarient);
                s.source.pitch = s.pitch * varient;
                s.source.loop = s.loop;
                s.source.Play();
                if (!s.source.loop)
                    soundGameObject.AddComponent<DestroySound>();
            }
        }
    }
}

