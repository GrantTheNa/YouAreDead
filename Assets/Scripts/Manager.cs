using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject serpentCoin;
    public GameObject fireCoin;

    //Post Fire Coin
    public GameObject F_SecretDoor;
    public GameObject F_SecretDoor2;
    public GameObject F_Fire;
    public GameObject F_HiddenPath;
    public GameObject F_Door;

    //
    public GameObject player;

    public bool shouldRespawn;

    public GameObject spawnPoint;

    //Dialogue
    public GameObject startingDialogue;
    public GameObject endDialogue;

    //coins
    int coinAmount = 0;

    //Post Serpant Coin
    public GameObject S_HiddenPath;
    public GameObject S_SecretDoor_1;
    public GameObject S_SecretDoor_2;

    public void FireCoin() //When Fire coin is collected
    {
        F_SecretDoor.SetActive(false);
        F_SecretDoor2.SetActive(false);
        F_Fire.SetActive(false);
        F_HiddenPath.SetActive(true);
        F_Door.SetActive(true);
        CheckCoins();
    }

    public void SerpentCoin() //When Serpent Coin is collected
    {
        S_HiddenPath.SetActive(true);
        S_SecretDoor_1.SetActive(false);
        S_SecretDoor_2.SetActive(false);
        CheckCoins();
    }

    public void CheckCoins() //Check if coins are both collected
    {
        coinAmount += 1;
        if (coinAmount >= 2)
        {
            SetUpEnd();
        }
    }

    private void SetUpEnd() //Make starting area a trigger to end the game
    {
        startingDialogue.SetActive(false);
        endDialogue.SetActive(true);
    }

    public void EndGame() //Trigger that ends the game
    {

    }

    public void KillPlayer()
    {
        player.GetComponent<PlayerControls>().shouldRespawn = true;
        Debug.Log("DIE");
        StartCoroutine(WaitRespawn());
    }

    //failsafe respawn
    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerControls>().shouldRespawn = false;
    }

    private void Update()
    {
        //if (player.transform.position == spawnPoint.transform.position)
        //{
        //    player.GetComponent<PlayerControls>().shouldRespawn = false;
        //    shouldRespawn = false;
        //}
    }

}
