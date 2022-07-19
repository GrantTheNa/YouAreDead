using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatenScene : MonoBehaviour
{
    //Monster
    public GameObject monsterObj;
    public Animator monster;
    public AnimationClip animation;

    //Player
    public PlayerControls pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        monster.SetTrigger("Trig_EatingCutscene");
        StartCoroutine(DeleteMonster());
    }

    IEnumerator DeleteMonster()
    {
        pc.canPlayerMove = false;
        yield return new WaitForSeconds(animation.length);
        pc.ArmEaten();
        pc.canPlayerMove = true;
        Destroy(monsterObj);
    }
}
