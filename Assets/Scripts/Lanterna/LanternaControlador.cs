using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternaControlador : MonoBehaviour
{
    public InputController inputController;
    public Light luz;

    void Start()
    {
      
    }


    void Update()
    {
        if (inputController.LightOnOff())
        {
            luz.enabled = !luz.enabled;
        }

    }
}
