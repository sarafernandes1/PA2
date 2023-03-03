using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarObjetos : MonoBehaviour
{
    public InputController inputController;
    int pista = 0;
    Vector3 positionFoward;

    void Start()
    {
        positionFoward = new Vector3(transform.position.x+1.0f, transform.position.y, transform.position.z + 1.0f);
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (inputController.PegarItem())
        {
            //positionFoward = new Vector3(transform.position.x+2.0f, transform.position.y, transform.position.z );
            // Destroy(other.gameObject);
            //other.transform.position = positionFoward;
            // Time.timeScale = 0.0f;
            pista = 1;
        }

        if(pista==1 && other.tag == "Porta" && inputController.PegarItem())
        {
            // Destroy(other.gameObject);
            //Destroy(other.transform.parent.gameObject);
            Destroy(other.transform.parent.gameObject);
        }
    }


}
