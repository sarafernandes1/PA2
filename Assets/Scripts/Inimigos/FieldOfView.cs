using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private NavMeshAgent agent;

    public bool canSeePlayer;

    public GameObject[] pontos;
    int pontos_index;
    bool perseguir = false;
    float distanceToPlayer;
    float displayTime = 2.0f;
    bool displayMessage = false;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void Update()
    {
        if (!perseguir) Move();
        if (perseguir)
        {
            float distance_min = 1000, distance;
            for (int i = 0; i < pontos.Length; i++)
            {
                distance = Vector3.Distance(transform.position, pontos[i].transform.position);
                if (distance < distance_min)
                {
                    distance_min = distance;
                    pontos_index = i;
                }
            }
            distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);
            agent.SetDestination(playerRef.transform.position);
            if (distanceToPlayer >= 5.0f)
            {
                perseguir = false;
            }

            if (distanceToPlayer <= 2.0f)
            {
                displayMessage = true;
            }
        }

        if (displayMessage)
        {
            displayTime -= Time.deltaTime;
            if (displayTime <= 0.0)
            {
                displayMessage = false;
                displayTime = 2.0f;
            }
        }
    }

    private void FieldOfViewCheck()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Collider[] rangeChecks = Physics.OverlapSphere(position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    agent.speed = 1.5f;
                    PlayerCaught();
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    void Move()
    {
        if (pontos_index < pontos.Length - 1)
        {
            agent.SetDestination(pontos[pontos_index].transform.position);

            float distance = Vector3.Distance(transform.position, pontos[pontos_index].transform.position);
            if (distance <= 0.6f)
            {
                pontos_index += 1;
            }
        }

        if (pontos_index == pontos.Length - 1) pontos_index = 0;
    }

    private void OnGUI()
    {
        if (displayMessage)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Jogador apanhado");
        }
    }

    void PlayerCaught()
    {
        perseguir = true;

    }
}