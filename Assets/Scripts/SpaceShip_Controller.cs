﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip_Controller : MonoBehaviour
{
    private Animator anim;
    public Vector2 start;
    public Vector2 preLanding;
    public Vector2 ground;
    public float startDelay;
    private float distance;
    public float velocity;
    private bool preLand;
    private bool landing;
    public Vector3 spaceShipScale;
    private float groundPositionY;
    private bool toSpace;
    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);//Change scale of spaceShip
        gameObject.transform.rotation = new Quaternion(0f, 0f, -45, 110);//Change rotation of spaceShip
        gameObject.transform.localPosition = start;//Change position of spaceShip
        anim.SetBool("Grounded", false);
        anim.SetBool("FirstReload", false);
        anim.SetBool("Idle", false);
        preLand = false;
        distance = 0;
        anim.SetBool("Flying", true);
        toSpace = true;
        groundPositionY = ground.y;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((Mathf.Abs(gameObject.transform.position.y - groundPositionY)));
        if (toSpace)
        {
            if (gameObject.transform.localPosition.Equals(preLanding))
            {
                preLand = true;
                gameObject.transform.localScale = spaceShipScale;
                gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                distance = velocity;
                toSpace = false;
            }
            else
            {
                gameObject.transform.localPosition = Vector2.MoveTowards(start, preLanding, distance);
                distance += velocity;
            }
        }
        if (preLand)
        {
            gameObject.transform.localPosition = Vector2.MoveTowards(preLanding, ground, distance);
            distance += velocity;
        }
        if ((Mathf.Abs(gameObject.transform.position.y-groundPositionY))<1f & (Mathf.Abs(gameObject.transform.position.y - groundPositionY))!=0 & !landing)
        {//if the distance between the spaceship and the ground is less than 1 and !=0 the object is landing
            preLand = false;
            landing = true;
            anim.SetBool("Flying", false);
            gameObject.transform.localPosition = Vector2.MoveTowards(preLanding, ground, distance);
            distance += velocity;
            StartCoroutine(WaitLanding());
        }
        if (landing)
        {
            gameObject.transform.localPosition = Vector2.MoveTowards(preLanding, ground, distance);
            distance += velocity;
        }
        if ((Mathf.Abs(gameObject.transform.position.y - groundPositionY)) == 0)
        {
            //anim.SetBool("Grounded", true);
            StartCoroutine(Wait());
        }
        
    }
    IEnumerator WaitLanding()
    {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Grounded", true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6.05f);
        anim.SetBool("FirstReload", true);
    }
}
