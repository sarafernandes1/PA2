using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEspelhos : MonoBehaviour
{
    public float angle;
    public InputController inputController;
    public Canvas canvas;

    bool isinArea=false;
    public bool bloqueado = false;
    int n_vez_direita=0, n_vez_esquerda=0;

    void Start()
    {
        
    }

    void Update()
    {
        if (inputController.PegarItem() && isinArea && !bloqueado)
        {
            if (n_vez_direita >= 3)
            {
                angle *= (-1);
                n_vez_direita = 0;
            }
            transform.Rotate(new Vector3(0.0f, angle, 0.0f));
            n_vez_direita += 1;
           
        }

        if (bloqueado) canvas.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bloqueado)
        {
            isinArea = true;
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isinArea = false;
        canvas.enabled = false;
    }

}
