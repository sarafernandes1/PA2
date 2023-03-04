using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class OpenKeypad : MonoBehaviour
{
    public Canvas Pegar, Keypad;
    public InputController inputController;
    public Camera MainCamera;

    void Start()
    {
        
    }

    void Update()
    {
        if(inputController.PegarItem())
        {
            
            Time.timeScale = 0.0f;
            Keypad.enabled = true;

        }

       
    }


    private void OnTriggerStay(Collider other)
    {
        Pegar.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Pegar.enabled = false;
    }

    
}
