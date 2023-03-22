using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject[] espacos_inventario;
    GameObject[] objetos_inventario;
    public ItensDados dados_itens;
    int numero_espacos_disponiveis = 6;
    bool novo_item = false;
    int i = 0;

    public InputController inputController;
    int pista = 0;
    string nome_item;

    void Start()
    {
        objetos_inventario = new GameObject[6];
    }

    void Update()
    {
        if (numero_espacos_disponiveis > 0 && novo_item)
        {
            espacos_inventario[6-numero_espacos_disponiveis].GetComponent<Image>().sprite = dados_itens.imagem_objeto;
            numero_espacos_disponiveis -= 1;
            novo_item = false;
        }

        switch(inputController.ItemMao())
        {
            case 0:
            objetos_inventario[0].active=!objetos_inventario[0].active;
            objetos_inventario[0].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 1:
                objetos_inventario[1].active = !objetos_inventario[1].active;
                objetos_inventario[1].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 2:
                objetos_inventario[2].active = !objetos_inventario[2].active;
                objetos_inventario[2].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 3:
                objetos_inventario[3].active = !objetos_inventario[3].active;
                objetos_inventario[3].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 4:
                objetos_inventario[4].active = !objetos_inventario[4].active;
                objetos_inventario[4].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 5:
                objetos_inventario[5].active = !objetos_inventario[5].active;
                objetos_inventario[5].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case 6:
                objetos_inventario[6].active = !objetos_inventario[6].active;
                objetos_inventario[6].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f);
                break;
            case -1:
                break;
        }
    }

    public void ItemColetado(ItensDados dados, GameObject objeto)
    {
        objeto.SetActive(false);
        objetos_inventario[i] = objeto;
        objetos_inventario[i].transform.SetParent(this.transform);
        objetos_inventario[i].GetComponent<Item>().enabled = false;
        i++;
        novo_item = true;
        dados_itens = dados;
    }

}
