using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoController : MonoBehaviour
{
    int p=0;
    bool perseguir;
    bool displayMessage = false;
    float displayTime = 2.0f;

    public void Patrulha(int pontos_index, GameObject[] pontos, NavMeshAgent agent, Transform inimigo) 
    {
        if (pontos_index < pontos.Length - 1)
        {
            agent.SetDestination(pontos[pontos_index].transform.position);

            float distance = Vector3.Distance(inimigo.position, pontos[pontos_index].transform.position);
            if (distance <= 0.6f)
            {
                pontos_index += 1;
            }
        }

        if (pontos_index == pontos.Length - 1) pontos_index = 0;

        p = pontos_index;
    }

    public int pontos()
    {
        return p;
    }

}
