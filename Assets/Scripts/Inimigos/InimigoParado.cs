using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoParado : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private NavMeshAgent agent;

    public bool canSeePlayer;

    bool perseguir = false;
    float displayTime = 2.0f;
    bool displayMessage = false;

    public int index;

    Vector3 initialPosition;
    Quaternion initialRotation;

    public bool parado = true;
    public bool destraido = false;
    public Vector3 posicao_destracao;
    public GameObject objetoDistracao;
    bool pararDistracao = false;


    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
        if (destraido)
        {
            perseguir = false;
            if(Vector3.Distance(transform.position, posicao_destracao) <= 2.0f)
            {
                agent.SetDestination(transform.position);
                StartCoroutine(pararSom());
                if (pararDistracao)
                {
                    objetoDistracao.GetComponent<Light>().enabled = false;
                    destraido = false;
                    parado = true;
                }
            }
            else agent.SetDestination(posicao_destracao);
        }

        if (perseguir)
        {
            playerRef.GetComponent<Disfarce>().perseguidoEstado[index] = true;
            float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

            agent.speed = 1.5f;
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
        else
        {
            if (!destraido)
            {
                //transform.Rotate(new Vector3(0.0f, Random.Range(-10, 10), 0.0f));
                if (transform.position != initialPosition)
                {
                    agent.SetDestination(initialPosition);

                }
                float d = Vector3.Distance(transform.position, initialPosition);
                if (d < 0.5f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, 1.5f);
                    parado = true;
                }
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


    IEnumerator pararSom()
    {
        yield return new WaitForSeconds(2.0f);
        pararDistracao = true;
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
        parado = false;
    }

}