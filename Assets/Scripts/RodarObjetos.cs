using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodarObjetos : MonoBehaviour
{
    public bool aviao = false;
    bool esquerda=false, direita = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aviao)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -4.0f * Time.deltaTime));
        }
        else
        {
            float angle = 0;
            if (transform.rotation.z >= 0.2)
            {
                esquerda = true;
                direita = false;
            }
            if (transform.rotation.z <= -0.2)
            {
                esquerda = true;
                direita = true;
            }

            if (direita)
            {
                angle = 12.0f * Time.deltaTime;
            }
            else
            {
                angle = -12.0f * Time.deltaTime;
            }

            transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }
    }
}
