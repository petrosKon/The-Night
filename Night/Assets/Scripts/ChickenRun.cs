﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRun : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float startWaitingTime = 2f;
    public Transform pointBottom, pointTop;

    private float waitTime;
    private Transform moveSpot;
    private float minΖ;
    private float maxΖ;
    private float minX;
    private float maxX;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitingTime;

        minΖ = pointBottom.position.z;
        maxΖ = pointTop.position.z;
        minX = pointBottom.position.x;
        maxX = pointTop.position.x;


        moveSpot = new GameObject().transform;
        moveSpot.position = new Vector3(Random.Range(minX, maxX),0f, Random.Range(minΖ, maxΖ));

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,moveSpot.position,speed * Time.deltaTime);
        Vector3 relativePos = moveSpot.position - transform.position;
        if(relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
       

        //case of a simple chicken!!
        animator.SetBool("Run", true);

        if (Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minΖ, maxΖ));
                waitTime = startWaitingTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                animator.SetBool("Run", false);
            }
        }       
    }

}
