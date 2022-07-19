using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public AudioSource voiceClip;
    public Text dialogueText;

    private Queue<string> sentences;
    private Queue<float> timer;


    //when the dialogue is going on already
    float tempTimer;

    //float timer
    public float setTimer;
    //public bool timeSet;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        timer = new Queue<float>();
    }

    private void Update()
    {
        //if(!timeSet)
        //{
            //timeSet = true;
            SetTimer();
        //}

    }

    public void StartDialogue (Dialogue dialogue)
    {
        timer.Clear();
        sentences.Clear();
            voiceClip.clip = dialogue.voice;
            voiceClip.Play();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            foreach (float waitTimer in dialogue.timer)
            {
                timer.Enqueue(waitTimer);
            }
            StartCoroutine(DisplayNextSentence());
    }


    IEnumerator DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;

        tempTimer = timer.Dequeue();
        setTimer = tempTimer;
        while (setTimer > 0)
        {
            yield return null;
        }
        StartCoroutine(DisplayNextSentence());

    }


    private void SetTimer()
    {
        if (setTimer > 0)
        {
            setTimer -= Time.deltaTime;
            Debug.Log(setTimer);
        }
        else
        {
            Debug.Log("Finished Time");

                dialogueText.text = "";
                nameText.text = "";
            //timeSet = false;
        }
    }
}
