using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    [SerializeField] GameController controller;
    private int destoreyedEnemyCount;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject); //denize d��en oyuncular yok edilir.
        destoreyedEnemyCount++; //denize d��en oyuncular i�in saya�

        if (collision.gameObject.CompareTag("Player") || (destoreyedEnemyCount == 10))  //Player denize d��erse veya t�m d��manlar yok edilirse oyun biter.
        {
            controller.GameOver();
        }

    }
}
