using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyMonsterRotateTest : MonoBehaviour
{
    [SerializeField] private Transform rotatePoint;
    //[SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;

    Vector3 rotAxis = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotatePoint.position, rotAxis, rotSpeed * Time.deltaTime);
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
