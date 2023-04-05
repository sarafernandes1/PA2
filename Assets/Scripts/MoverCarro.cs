using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCarro : MonoBehaviour
{
    public InputController inputController;
    bool inArea = false;

    void Start()
    {

    }

    void Update()
    {
        if (inArea)
        {
            if (inputController.PegarItem())
            {
                transform.Rotate(0.0f, 10.0f, 0.0f);
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
