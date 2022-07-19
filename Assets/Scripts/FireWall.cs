using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public GameObject[] flamePrefabs;

    //Turn on Area
    public GameObject showRoom;

    public void CheckFlame()
    {
        int amount = 0;
        for (int i = 0; i < flamePrefabs.Length; i++)
        {
            amount += 1;
            Debug.Log(amount);
            if (flamePrefabs[i].GetComponent<SetOnFire>().onFire == false)
            {
                Debug.Log("EHHH");
                break;
            }
            if (amount >= flamePrefabs.Length)
                DestroyDoor();
        }

    }

    private void DestroyDoor()
    {

        showRoom.SetActive(true);
        Destroy(this.gameObject);
    }
}
