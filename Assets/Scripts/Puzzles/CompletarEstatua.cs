using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletarEstatua : MonoBehaviour
{
    public GameObject[] pecas_estatua;
    
    public InputController inputController;
    public GameObject player;  
    public GameObject porta;
    public Canvas canvas;

    GameObject peca_jogador_mao;
    int index;
    int numero_pecas = 0;
    bool isInArea = false;

    public bool alavanca;

    void Start()
    {
        
    }

    void Update()
    {
        if(isInArea && inputController.PegarItem())
        {
            peca_jogador_mao = player.gameObject.GetComponent<Inventario>().objeto_mao;
            index = player.gameObject.GetComponent<Inventario>().index;
           if(!alavanca) ColocarPecaestatua();
            else
            {
                if (peca_jogador_mao.name == "Alavanca")
                {
                    player.GetComponent<Inventario>().RetirarItemInventario(index);
                    Destroy(peca_jogador_mao);
                    pecas_estatua[0].GetComponentInChildren<MeshRenderer>().enabled = true;
                    numero_pecas += 1;
                }
            }
        }

        if (numero_pecas==2)
        {
            Destroy(porta.gameObject);
        }

        if(numero_pecas==1 && alavanca)
        {
            gameObject.GetComponent<Alavanca>().a = true;
            StartCoroutine(desativar());
        }
    }

        IEnumerator desativar()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CompletarEstatua>().enabled = false;

    }

    public void ColocarPecaestatua()
    {
        int i = 0;
            if (peca_jogador_mao.name== "US_Sword_")
            {
                i = 0;
               player.GetComponent<Inventario>().RetirarItemInventario(index);
                Destroy(peca_jogador_mao);
                pecas_estatua[i].SetActive( true);
                //pecas_estatua[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                numero_pecas += 1;
            }

            if (peca_jogador_mao.tag == "Escudo")
            {
                i = 1;
                player.GetComponent<Inventario>().RetirarItemInventario(index);
                Destroy(peca_jogador_mao);
                pecas_estatua[i].SetActive(true);
                //pecas_estatua[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                numero_pecas += 1;
            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isInArea = true;
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInArea = false;
        canvas.enabled = false;

    }
}
