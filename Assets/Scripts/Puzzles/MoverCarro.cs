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
    bool rodar=false, rodarBarco=false;
    public Canvas canvas;
    public bool carro_, barco_;
    public int n_vezes;

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

        if (rodarBarco)
        {
                engine[0].transform.Rotate(0, 0, 10.0f * Time.deltaTime);
            
            carro.transform.Rotate(0.0f, 0.6f * Time.deltaTime, 0.0f);
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
            if (inArea/* && GetComponent<MeshRenderer>().enabled*/ && carro_)
            {

                if (inputController.PegarItem() && !rodar)
                {
                rodar = true;
                
                StartCoroutine(espera());
            }

            
            }
        //}
        //if(colocado)  StartCoroutine(espera());

        if (inArea/* && GetComponent<MeshRenderer>().enabled*/ && barco_ && n_vezes>0)
        {

            if (inputController.PegarItem() && !rodarBarco)
            {


                StartCoroutine(espera1());
                //rodarBarco = true;
                n_vezes--;
            }


        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2.0f);
        rodar = false;
    }

    IEnumerator espera1()
    {
        rodarBarco = true;
        yield return new WaitForSeconds(2f);
        rodarBarco = false;
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
