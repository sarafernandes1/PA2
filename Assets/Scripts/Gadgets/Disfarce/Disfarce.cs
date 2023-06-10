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

    public Canvas canvas;
    public bool[] perseguidoEstado;

    public Text tempoText;
    bool perseguido;

    public bool ativo = false;
    public Image image;
    public GameObject player_modelo, guarda_modelo;
    public Material material_dissolve;
    public Material[] material_original;

    public Material material_dissolve_guarda;
    public Material[] material_original_guarda;


    float tempo_shader;
    bool shader = true;

    void Start()
    {
        perseguidoEstado = new bool[2];
        tempo_shader = 0.4f + disfarceTimer / 10f;

    }


    void Update()
    {
     
       material_dissolve.SetFloat("_Tempo", tempo_shader);
        material_dissolve_guarda.SetFloat("_Tempo", tempo_shader);

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
               
                if(a) aplicarDissolveShader();
                image.color = Color.red;
                a = false;
                canvas.enabled = true;
                if (this.gameObject.layer != 6) gameObject.layer = 6;
                tempoText.text = ((int)disfarceTimer).ToString();
                disfarceTimer -= Time.deltaTime;
                if(shader) tempo_shader -= Time.deltaTime;

                if (tempo_shader <= 0)
                {
                    shader = false;
                    tempo_shader = 1;
                }

                if (disfarceTimer <= 1.5 && disfarceTimer>=0.8f)
                {
                    shader = true;
                    if(shader) tempo_shader -= Time.deltaTime;
                    desativarDissolveShader();
                
                }
                if (disfarceTimer <= 0 )
                {
                    gameObject.layer = 3;
                    disfarceTimer = 6.0f;
                    tempo_shader = 1;
                    
                    disfarceEstado = false;
                    canvas.enabled = false;
                    tempoText.text = "";
                    a = true;
                    ativo = false;
                    image.color = Color.white;
                    shader = true;
                    //gameObject.GetComponent<Disfarce>().enabled = false;

                }
            }
        }

     
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2.0f);
        a = true;
    }

    public void aplicarDissolveShader()
    {
        material_dissolve.SetFloat("_Ativo", 1);
       
        Material[] mats = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = material_dissolve;
        mats[0] = material_dissolve;
        mats[2] = material_dissolve;

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats;

        StartCoroutine(TrocaPersonagens(false, true));
    }

    public void desativarDissolveShader()
    {
       
        material_dissolve_guarda.SetFloat("_Ativo", 1);

     
        Material[] mats1 = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats1[1] = material_dissolve_guarda;
        mats1[0] = material_dissolve_guarda;
        mats1[2] = material_dissolve_guarda;

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats1;
        //material_dissolve.SetFloat("_Tempo", segundo_tempo_shader);


        StartCoroutine(Dissolve());

        
        StartCoroutine(TrocaPersonagens1(true,false));

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

    IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(1.2f);
        
        material_dissolve_guarda.SetFloat("_Ativo", 0);
        Material[] mats = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = material_original[1];
        mats[0] = material_original[0];
        mats[2] = material_original[2];

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats;
    }

   

    IEnumerator TrocaPersonagens(bool player, bool guarda)
    {
        yield return new WaitForSeconds(0.8f);
        material_dissolve.SetFloat("_Ativo", 0);
        Material[] mats = player_modelo.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = material_original[1];
        mats[0] = material_original[0];
        mats[2] = material_original[2];

        player_modelo.GetComponent<SkinnedMeshRenderer>().materials = mats;
        player_modelo.SetActive(player);
        guarda_modelo.SetActive(guarda);

    }

    IEnumerator TrocaPersonagens1(bool player, bool guarda)
    {
        yield return new WaitForSeconds(0.5f);
        material_dissolve.SetFloat("_Ativo", 0);
       
        player_modelo.SetActive(player);
        guarda_modelo.SetActive(guarda);

    }
}
