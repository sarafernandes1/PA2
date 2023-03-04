using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject[] espacos_inventario;
    public InputController inputController;
    public GameObject mao;
    GameObject[] objetos_inventario;
    public ItensDados dados_itens;

    int numero_espacos_disponiveis = 6;
    bool novo_item = false;
    int index_imagem = 0;
    bool displayMessage = false;
    float displayTime = 3;
    bool ativo;

    void Start()
    {
        objetos_inventario = new GameObject[6];
    }

    void Update()
    {
        ConfirmarInventario();

        if (numero_espacos_disponiveis > 0 && novo_item)
        {
            espacos_inventario[index_imagem].GetComponent<Image>().sprite = dados_itens.imagem_objeto;
            numero_espacos_disponiveis -= 1;
            novo_item = false;
        }

        if (displayMessage)
        {
            displayTime -= Time.deltaTime;
            if (displayTime <= 0.0)
            {
                displayMessage = false;
                displayTime = 3.0f;
            }
        }

        if (inputController.DescartarItem())
        {
            for (int l = 0; l < 6; l++)
            {
                if (objetos_inventario[l] != null)
                {
                    if (objetos_inventario[l].activeInHierarchy)
                    {
                        RetirarItemInventario(l);
                        if (numero_espacos_disponiveis < 6) numero_espacos_disponiveis += 1;
                    }
                }
            }
        }

        switch (inputController.ItemMao())
        {
            case 0:
                if (!Verificar(0)) AtivarItemMao(0);
                break;
            case 1:
                if (!Verificar(1)) AtivarItemMao(1);

                break;
            case 2:
                if (!Verificar(2)) AtivarItemMao(2);

                break;
            case 3:
                if (!Verificar(3)) AtivarItemMao(3);

                break;
            case 4:
                if (!Verificar(4)) AtivarItemMao(4);
                break;
            case 5:
                if (!Verificar(5)) AtivarItemMao(5);
                break;
            case -1:
                break;
        }
    }

    // quando um item é coletado, passa a ser filho do jogador 
    public void ItemColetado(ItensDados dados, GameObject objeto)
    {
        if (numero_espacos_disponiveis > 0)
        {
            objeto.SetActive(false);
            for (int i = 0; i < 6; i++)
            {
                if (objetos_inventario[i] == null)
                {
                    objetos_inventario[i] = objeto;
                    objetos_inventario[i].transform.SetParent(this.transform);
                    objetos_inventario[i].GetComponent<Item>().enabled = false;
                    index_imagem = i;
                    break;
                }
            }
            novo_item = true;
            dados_itens = dados;
        }
        else
        {
            displayMessage = true;
        }
    }

    private void OnGUI()
    {
        if (displayMessage)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Sem espaços disponiveis");
        }
    }

    // caso algum item seja utilizado num puzzle, o seu pai não é o jogador, o item é retirado
    void ConfirmarInventario()
    {
        for (int i = 0; i < 6; i++)
        {
            if (objetos_inventario[i] != null)
            {
                if (objetos_inventario[i].transform.parent != this.transform)
                {
                    RetirarItemInventario(i);
                    if (numero_espacos_disponiveis < 6) numero_espacos_disponiveis += 1;
                }
            }
        }
    }

    //  verificar se item está na mão do jogador, para que este não tenha 2 ou mais itens na mão
    bool Verificar(int index)
    {
        bool tem_na_mao = false;
        for (int i = 0; i < 6; i++)
        {
            if (objetos_inventario[i] != null)
            {
                if (i != index && objetos_inventario[i].activeInHierarchy)
                {
                    tem_na_mao = true;
                }
            }
        }
        return tem_na_mao;
    }

    void AtivarItemMao(int index)
    {
        ativo = !ativo;
        objetos_inventario[index].SetActive(ativo);
        objetos_inventario[index].transform.position = mao.transform.position;
    }

    void RetirarItemInventario(int index)
    {
        objetos_inventario[index].GetComponent<Item>().enabled = true;
        objetos_inventario[index].transform.SetParent(null);
        espacos_inventario[index].GetComponent<Image>().sprite = null;
        objetos_inventario[index].transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z + 1.0f);
        objetos_inventario[index] = null;
    }
}
