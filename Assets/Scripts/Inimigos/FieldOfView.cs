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
    public int pontos_index;
    bool perseguir = false;
    float displayTime = 2.0f;
    bool displayMessage = false;

    InimigoController inimigo;
    public bool a;
    public int index;

    public bool batalhaFinal=false;


    private void Start()
    {
        inimigo = new InimigoController();
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
        if (!batalhaFinal)
        {
            if (!perseguir)
            {
                playerRef.GetComponent<Disfarce>().perseguidoEstado[index] = false;

                Move();
            }
            else
            {
                playerRef.GetComponent<Disfarce>().perseguidoEstado[index] = true;
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
                float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

                // agent.speed = 2.0f;
                agent.SetDestination(playerRef.transform.position);

                if (distanceToPlayer >= 10.0f)
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
                    StartCoroutine(espera());
                    displayTime = 2.0f;
                }
            }
        }
        else
        {
            agent.SetDestination(playerRef.transform.position);
            agent.speed = 4;
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

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2.0f);
        GameOver.gameOver = true;

    }

    void Move()
    {

        //agent.speed = 1.5f;
        inimigo.Patrulha(pontos_index, pontos, agent, this.transform);
        pontos_index = inimigo.pontos();
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
