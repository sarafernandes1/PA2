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

    public GameObject objeto_mao;

    int numero_espacos_disponiveis = 6;
    bool novo_item = false;
    int index_imagem = 0;
    bool displayMessage = false;
    float displayTime = 3;
    bool ativo;

    public int index = -1;

    public float item;

    bool itens_desativados = false;

    public Image[] inventarioImagens;
    public bool temObjetoFinal = false;

    void Start()
    {
        objetos_inventario = new GameObject[6];
    }

    void Update()
    {

        //if(temObjetoFinal)
        //{
        //    verificarItemInventario();
        //}


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


        if (inputController.ItemInventario())
        {
            if ((index + 1) < objetos_inventario.Length)
            {
                if (objetos_inventario[index + 1] != null) index += 1;
                else if ((index + 2) < objetos_inventario.Length && objetos_inventario[index + 2] != null)
                {
                    index += 2;
                }
                else if ((index + 3) < objetos_inventario.Length && objetos_inventario[index + 3] != null)
                {
                    index += 3;
                }
                else if ((index + 4) < objetos_inventario.Length && objetos_inventario[index + 4] != null)
                {
                    index += 4;
                }
                else if ((index + 5) < objetos_inventario.Length && objetos_inventario[index + 5] != null)
                {
                    index += 5;
                }
                else if ((index + 6) < objetos_inventario.Length && objetos_inventario[index + 6] != null)
                {
                    index += 6;
                }
                else
                {
                    index = (-1);
                }
            }
            else
            {
                index = (-1);
            }
            if (index != -1)
            {
                objeto_mao = objetos_inventario[index];
                AtivarItemMao(index);
            }
            else
            {
                DestaivarItemMao();
            }
        }

    }

    public void escolherItem()
    {
        switch (index)
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
        }
    }

    // quando um item é coletado, passa a ser filho do jogador 
    public void ItemColetado(ItensDados dados, GameObject objeto)
    {
        if (numero_espacos_disponiveis > 0)
        {
            if (objeto.name == "Artifact_Ring_") temObjetoFinal = true;
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
    bool Verificar(int index_)
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

    void AtivarItemMao(int index_)
    {
        for (int i = 0; i < objetos_inventario.Length; i++)
        {
            if (objetos_inventario[i] != null)
            {
                if (i == index)
                {
                    objetos_inventario[i].SetActive(true);
                    objetos_inventario[index_].transform.position = mao.transform.position;

                    inventarioImagens[i].color = Color.red;

                }
                else
                {
                    objetos_inventario[i].SetActive(false);
                    inventarioImagens[i].color = Color.white;
                }
            }
        }
    }

    void DestaivarItemMao()
    {
        for (int i = 0; i < objetos_inventario.Length; i++)
        {
            if (objetos_inventario[i] != null)
            {

                objetos_inventario[i].SetActive(false);
                inventarioImagens[i].color = Color.white;

            }
            else
            {
                continue;
            }
        }
    }

    public void RetirarItemInventario(int index_)
    {
        objetos_inventario[index_].GetComponent<Item>().enabled = true;
        objetos_inventario[index_].transform.SetParent(null);
        espacos_inventario[index_].GetComponent<Image>().sprite = null;
        objetos_inventario[index_].transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z + 1.0f);
        objetos_inventario[index_] = null;
        inventarioImagens[index_].color = Color.white;
        index_ = -1;
    }
    

    public void verificarItemInventario()
    {
        GameObject[] guardas = GameObject.FindGameObjectsWithTag("Guardas");
        foreach (var guarda in guardas)
        {
            guarda.GetComponent<FieldOfView>().batalhaFinal = true;
        }
    }
}
