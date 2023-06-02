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
    public Image image;
    public GameObject player_modelo;
    public Material material_dissolve;
    public Material[] material_original;

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
            else
            {
                disfarceEstado = false;
                ativo = false;
            }

            if (disfarceEstado)
            {
                aplicarDissolveShader();
                image.color = Color.red;
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
                    image.color = Color.white;
                    //gameObject.GetComponent<Disfarce>().enabled = false;
                    desativarDissolveShader();
                }
            }
        }

    }

    public void aplicarDissolveShader()
    {
        Material[] mats = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = material_dissolve;
        mats[0] = material_dissolve;
        mats[2] = material_dissolve;

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats;
    }

    public void desativarDissolveShader()
    {
        Material[] mats = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = material_original[1];
        mats[0] = material_original[0];
        mats[2] = material_original[2];

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats;
    }

    bool verificarGuardas()
    {
        for (int i = 0; i < perseguidoEstado.Length; i++)
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
