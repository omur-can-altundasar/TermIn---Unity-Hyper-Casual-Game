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
        if (agent.enabled == true)  /*NavMeshAgent aktifse yak�n�ndaki d��man� takip eder*/
        {
            FollowNearestEnemy();
        }

        if (agentRb.velocity.z == isStatic.z)   /*Agent tamamen durgun duruma gelince tekrar NavMeshAgent �zelli�i aktif edilir.*/
        {
            agent.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) //Agent, �arpt��� d��manlara kuvvet uygular.
        {                                                                   //�arpt���nda fizik kanunlar� devre girmesi i�in NavMeshAgent bile�enini devred��� b�rakmal�.
            agent.enabled = false;
            agentRb.AddRelativeForce(1000 * Time.deltaTime * Vector3.back);
        }

        else if (collision.gameObject.CompareTag("Sea"))
        {
            agent.enabled = false;      //Oyuncular denize d��t�k�e sonra Player'a skor yaz�l�r.
            controller.ScoreAdd();
        }
    }
    void FollowNearestEnemy()
    {   /*Her agent kendisine en yak�n mesafedeki oyuncuyu bulur ve hedef olarak belirler.*/
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