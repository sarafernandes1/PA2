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
    public GameObject[] engine;
    bool rodar=false;
    public Canvas canvas;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (rodar)
        {
            engine[0].transform.Rotate(0, 0, 10.0f * Time.deltaTime);
            engine[1].transform.Rotate(0, 0, -10.0f * Time.deltaTime);
            engine[2].transform.Rotate(0, 0, 20.0f * Time.deltaTime);
            carro.transform.Rotate(0.0f, 10.0f * Time.deltaTime, 0.0f);
        }
    }

    void Update()
    {
        //if (inArea && !GetComponent<MeshRenderer>().enabled)
        //{
        //    if (inputController.PegarItem() && player.GetComponent<Inventario>().objeto_mao.name==gameObject.name)
        //    {
        //        int index = player.gameObject.GetComponent<Inventario>().index;
        //        player.GetComponent<Inventario>().RetirarItemInventario(index);
        //        Destroy(player.GetComponent<Inventario>().objeto_mao);
        //        GetComponent<MeshRenderer>().enabled = true;
        //        colocado = true;
        //    }
        //}

        //if (!colocado)
        //{
            if (inArea/* && GetComponent<MeshRenderer>().enabled*/)
            {

                if (inputController.PegarItem() && !rodar)
                {
                rodar = true;
                
                StartCoroutine(espera());
            }

            
            }
        //}
       //if(colocado)  StartCoroutine(espera());
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2.0f);
        rodar = false;
    }

    private void OnTriggerStay(Collider other)
    {
        inArea = true;
        canvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;
        canvas.enabled = false;

    }
}
