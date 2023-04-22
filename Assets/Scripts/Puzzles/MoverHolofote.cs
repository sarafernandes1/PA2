using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHolofote : MonoBehaviour
{
    public InputController inputController;
    bool inArea = false;
    public Light luz;

    void Start()
    {
        
    }

    void Update()
    {
        if (inArea)
        {
            if (inputController.PegarItem())
            {
                luz.transform.Rotate(0.0f, 0.0f, 10.0f);
            }
        }
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
