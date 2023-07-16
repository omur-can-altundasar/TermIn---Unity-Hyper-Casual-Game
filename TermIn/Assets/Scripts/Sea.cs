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
        Destroy(collision.gameObject); //denize düþen oyuncular yok edilir.
        destoreyedEnemyCount++; //denize düþen oyuncular için sayaç

        if (collision.gameObject.CompareTag("Player") || (destoreyedEnemyCount == 10))  //Player denize düþerse veya tüm düþmanlar yok edilirse oyun biter.
        {
            controller.GameOver();
        }

    }
}
