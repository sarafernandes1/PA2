using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    bool virado = false;
    public bool podeVirar = true;

    public GameObject luz;
    public Material luzApagada;
    public Material luzLigada;

    public GameObject controlador;

    bool inArea = false;
    public InputController inputController;

    public bool a = true;

    IEnumerator mudar()
    {
        yield return new WaitForSeconds(0.875f);
        podeVirar = true;
        virado = !virado;

        if (virado)
        {
            luz.GetComponent<Renderer>().material = luzLigada;
        }
        else
        {
            luz.GetComponent<Renderer>().material = luzApagada;
        }

        controlador.GetComponent<AlavancaManager>().receberSinal(gameObject, virado);
    }

    private void OnTriggerEnter(Collider other)
    {
        inArea = true;

    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;
    }

    private void Update()
    {
        if (inArea && a)
        {
            if (inputController.PegarItem())
            {
                if (podeVirar)
                {
                    podeVirar = false;
                    if (!virado)
                    {
                        GetComponent<Animation>().Play("Alavanca|Desligar");
                        StartCoroutine(mudar());
                    }
                    else
                    {
                        GetComponent<Animation>().Play("Alavanca|Ligar");
                        StartCoroutine(mudar());

                    }
                }
            }
        }
    }

    //void OnMouseDown()
    //{
    //    if (podeVirar)
    //    {
    //        podeVirar = false;
    //        if (!virado)
    //        {
    //            GetComponent<Animation>().Play("Sphere|Descer");
    //            StartCoroutine(mudar());
    //        }
    //        else
    //        {
    //            GetComponent<Animation>().Play("Sphere|Subir");
    //            StartCoroutine(mudar());

    //        }
    //    }

    //}
}