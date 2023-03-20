using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disfarce : MonoBehaviour
{
    public InputController inputController;
    float disfarceTimer = 6.0f;
    public bool disfarceEstado = false;
    public bool a = true;

    bool disfarcePaused = false;
    float disfarcePausedTimer = 10.0f;

    public Canvas canvas;
    public bool[] perseguidoEstado;

    public Text tempoText;
    bool perseguido;

    public bool ativo = false;

    void Start()
    {
        perseguidoEstado = new bool[2];
    }


    void Update()
    {
        if (ativo)
        {
            if (!verificarGuardas())
            {
                disfarceEstado = true;
            }

            if (disfarceEstado)
            {
                a = true;
                canvas.enabled = true;
                if (this.gameObject.layer != 6) gameObject.layer = 6;
                tempoText.text = ((int)disfarceTimer).ToString();
                disfarceTimer -= Time.deltaTime;
                if (disfarceTimer <= 0)
                {
                    gameObject.layer = 3;
                    disfarceTimer = 6.0f;
                    disfarceEstado = false;
                    canvas.enabled = false;
                    tempoText.text = "";
                    a = false;
                    ativo = false;
                    gameObject.GetComponent<Disfarce>().enabled = false;
                }
            }
        }

    }

    bool verificarGuardas()
    {
        for(int i = 0; i < perseguidoEstado.Length; i++)
        {
            if (perseguidoEstado[i] == true) return true;
        }
        return false;
    }

    public void GuardaPersegueJogador(bool valor, int index)
    {
        perseguidoEstado[index] = valor;
    }
}
