using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TheAgents : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform nearestEnemy;
    private Rigidbody agentRb;
    private Vector3 isStatic = new(0f, 0f, 0f);
    [SerializeField] List<Transform> EnemyList;
    [SerializeField] GameController controller;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (agent.enabled == true)  /*NavMeshAgent aktifse yakýnýndaki düþmaný takip eder*/
        {
            FollowNearestEnemy();
        }

        if (agentRb.velocity.z == isStatic.z)   /*Agent tamamen durgun duruma gelince tekrar NavMeshAgent özelliði aktif edilir.*/
        {
            agent.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) //Agent, çarptýðý düþmanlara kuvvet uygular.
        {                                                                   //Çarptýðýnda fizik kanunlarý devre girmesi için NavMeshAgent bileþenini devredýþý býrakmalý.
            agent.enabled = false;
            agentRb.AddRelativeForce(1000 * Time.deltaTime * Vector3.back);
        }

        else if (collision.gameObject.CompareTag("Sea"))
        {
            agent.enabled = false;      //Oyuncular denize düþtükçe sonra Player'a skor yazýlýr.
            controller.ScoreAdd();
        }
    }
    void FollowNearestEnemy()
    {   /*Her agent kendisine en yakýn mesafedeki oyuncuyu bulur ve hedef olarak belirler.*/
        float minimumDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (Transform enemy in EnemyList)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(agent.transform.position, enemy.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = enemy;
                    agent.SetDestination(nearestEnemy.transform.position);
                }
            }
        }
    }
}