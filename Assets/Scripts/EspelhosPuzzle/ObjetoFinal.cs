using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoFinal : MonoBehaviour
{
    public LineRenderer[] linhas_espelhos;
    public bool[] espelhos_conectados;
    public GameObject porta;
    public static bool estatuaCompleta = false;
    public bool lasers = false;

    public Animator animator;


    void Start()
    {
        espelhos_conectados = new bool[linhas_espelhos.Length]; 
    }

    void Update()
    {
        for(int i = 0; i < linhas_espelhos.Length; i++)
        {
            float distance = Vector3.Distance(linhas_espelhos[i].GetPosition(linhas_espelhos[i].positionCount - 1), transform.position);
            if(distance<=1.0f)
            {
                espelhos_conectados[i] = true;
            }
            else
            {
                espelhos_conectados[i] = false;
            }
        }

        for (int i = 0; i < 1; i++)
        {
            if (espelhos_conectados[i] && espelhos_conectados[i+1])
            {
                // Destroy(porta.gameObject);
                lasers = true;
            }
        }

        if(lasers && estatuaCompleta)
        {
            animator.SetBool("Move", true);
        }

    }
}
