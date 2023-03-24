using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletarEstatua : MonoBehaviour
{
    public MeshRenderer[] pecas_estatua;
    
    public InputController inputController;
    public GameObject player;  
    public GameObject porta;
    public Canvas canvas;

    GameObject peca_jogador_mao;
    int index;
    int numero_pecas = 0;
    bool isInArea = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(isInArea && inputController.PegarItem())
        {
            peca_jogador_mao = player.gameObject.GetComponent<Inventario>().objeto_mao;
            index = player.gameObject.GetComponent<Inventario>().index;
            ColocarPecaestatua();
        }

        if (numero_pecas==3)
        {
            Destroy(porta.gameObject);
        }
    }

    public void ColocarPecaestatua()
    {
        for (int i = 0; i < pecas_estatua.Length; i++) {
            if (peca_jogador_mao.name== pecas_estatua[i].name)
            {
               player.GetComponent<Inventario>().RetirarItemInventario(index);
                Destroy(peca_jogador_mao);
                pecas_estatua[i].enabled = true;
                numero_pecas += 1;
            }
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
        canvas.enabled = true;

    }
}
