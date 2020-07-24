﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator anim;
    public float enemyContact = 3f;//enemy lifes
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("EnemyLife2", enemyContact);
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.
            enemyContact--;
            anim.SetBool("HitDamage2", true);
            anim.SetFloat("EnemyLife2", enemyContact);
        }
        
        if (enemyContact < 0.1f)
        {
            //Enemy die, here you can put the animation.
            anim.SetFloat("EnemyLife2", enemyContact);
            yield return new WaitForSeconds(1.2f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("projectil"))
        {
            //Enemy damaged, here you can put the animation.

            anim.SetBool("HitDamage2", false);

        }
    }
}