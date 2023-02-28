using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public GameObject[] pontos;
    int pontos_index;
    bool perseguir = false;
    float distanceToPlayer;
    float displayTime = 2.0f;
    bool displayMessage = false;

    private void Start()
    {
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
            distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);
            transform.LookAt(playerRef.transform.position);
            transform.position += transform.forward * 2.0f * Time.deltaTime;
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
            //Vector3 targetInVec3 = new Vector3(waypoints[waypointIndex].transform.position.x, waypoints[waypointIndex].transform.position.y, 0f);

            //Vector3 direction2 = targetInVec3 - transform.position;

            //float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg - 90;

            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //transform.rotation = rotation;


            transform.position = Vector3.MoveTowards(transform.position, pontos[pontos_index].transform.position, 2.0f * Time.deltaTime);
            transform.LookAt(pontos[pontos_index].transform.position);
            if (transform.position == pontos[pontos_index].transform.position)
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
