using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCarro : MonoBehaviour
{
    public InputController inputController;
    bool inArea = false;
    public GameObject carro;
    bool colocado = false;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (inArea && !GetComponent<MeshRenderer>().enabled)
        {
            if (inputController.PegarItem() && player.GetComponent<Inventario>().objeto_mao.name==gameObject.name)
            {
                int index = player.gameObject.GetComponent<Inventario>().index;
                player.GetComponent<Inventario>().RetirarItemInventario(index);
                Destroy(player.GetComponent<Inventario>().objeto_mao);
                GetComponent<MeshRenderer>().enabled = true;
                colocado = true;
            }
        }

        if (!colocado)
        {
            if (inArea && GetComponent<MeshRenderer>().enabled)
            {
                if (inputController.PegarItem())
                {
                    carro.transform.Rotate(0.0f, 10.0f, 0.0f);
                }
            }
        }

       if(colocado)  StartCoroutine(espera());
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(1.0f);
        colocado = false;
    }

    private void OnTriggerStay(Collider other)
    {
        inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;

    }
}
