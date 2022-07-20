using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatenScene : MonoBehaviour
{
    //Monster
    public GameObject monsterObj;
    public Animator monster;
    public AnimationClip animation;
    public ParticleSystem bloodParticle;

    //Player
    public PlayerControls pc;

    Animator animator;

    private void Start()
    {
        pc = FindObjectOfType<PlayerControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator = other.gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("isScared", true);

        monster.SetTrigger("Trig_EatingCutscene");
        StartCoroutine(DeleteMonster(animator));

        //spawn blood test
        if (bloodParticle != null)
            Instantiate(bloodParticle, other.gameObject.transform);
        
    }

    IEnumerator DeleteMonster(Animator anim)
    {
        pc.canPlayerMove = false;
        yield return new WaitForSeconds(animation.length);
        pc.ArmEaten();
        pc.canPlayerMove = true;
        Destroy(monsterObj);

        anim.SetBool("isScared", false);
    }
}
