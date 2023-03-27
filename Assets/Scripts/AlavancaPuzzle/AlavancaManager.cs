using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlavancaManager : MonoBehaviour
{
    public GameObject[] alavancas;

    public bool[] codigo;

    bool[] nossoCodigo;

    public GameObject[] objCertos;
    public string[] funcoes;

    void Start()
    {
        nossoCodigo = new bool[codigo.Length];
    }

    void seCerto()
    {
        for (int i = 0; i < objCertos.Length; i++)
        {
            objCertos[i].GetComponent<codigoCerto>().SendMessage(funcoes[i]);
        }
    }

    public void receberSinal(GameObject go, bool estado)
    {
        for (int i = 0; i < alavancas.Length; i++)
        {
            if (go == alavancas[i])
            {
                nossoCodigo[i] = estado;
                break;
            }
        }
        verificar();
    }

    void verificar()
    {
        bool certo = true;

        for (int i = 0; i < codigo.Length; i++)
        {
            if (nossoCodigo[i] != codigo[i])
            {
                certo = false;
                break;
            }
        }

        if (certo)
        {
            foreach (GameObject a in alavancas)
            {
                a.GetComponent<Alavanca>().podeVirar = false;
            }
            seCerto();
        }
    }


}
