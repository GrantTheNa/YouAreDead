using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
    public float[] timer;

    public AudioClip voice;
    public AudioClip voiceInt;
}

