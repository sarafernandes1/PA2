using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    bool inArea = false;
    private InputController inputController;
    private GameObject player;

    GameObject peca_jogador_mao;
    int index;

    void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(inArea && inputController.PegarItem())
        {
                peca_jogador_mao = player.gameObject.GetComponent<Inventario>().objeto_mao;

                index = player.gameObject.GetComponent<Inventario>().index;
              
                    if (peca_jogador_mao.name == "Chave")
                    {
                Destroy(this.gameObject);
                    }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;
    }

}
