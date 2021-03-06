﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 2f;//enemy lifes
    public AudioClip hitSound;
    private bool canbedmged;
    private GameObject shield;
    private AudioSource soundEffects;

    void Start()
    {
        anim = GetComponent<Animator>();
        soundEffects = GetComponent<AudioSource>();
        anim.SetFloat("EnemyLife", enemyContact);
        try
        {
            shield = transform.GetChild(0).gameObject;
            var codechild = shield.GetComponent<littleEnemy>();
            canbedmged = codechild.abletobedmg;
        }
        catch { canbedmged = true; }
    }
    private void Update()
    {
        var shield = transform.GetChild(0).gameObject;
        if (shield.activeSelf)
        {
            var codechild = shield.GetComponent<littleEnemy>();
            canbedmged = codechild.abletobedmg;
        }
        else
        {
            canbedmged = true;
        }
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
	{
	    if (collision.collider.CompareTag("projectil") && canbedmged){
            //Enemy damaged, here you can put the animation.
            soundEffects.clip = hitSound;
            soundEffects.Play();
            enemyContact--;
            anim.SetBool("HitDamage",true);
            anim.SetFloat("EnemyLife", enemyContact);
        }
        else
        {
            if (canbedmged)
            {
                //Enemy damaged, here you can put the animation.
                if (collision.relativeVelocity.magnitude > 1.5f)
				{
                    soundEffects.clip = hitSound;
                    soundEffects.Play();
                }
                enemyContact -= 0.5f;
                anim.SetBool("HitDamage", false);
                anim.SetFloat("EnemyLife", enemyContact);
            }
        }
        if (enemyContact <= 0)
        {
            //Enemy die, here you can put the animation.
            
            anim.SetFloat("EnemyLife", enemyContact);
            yield return new WaitForSeconds(0.58f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
         
            anim.SetBool("HitDamage", false);
            
        }
    }
}
