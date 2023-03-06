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
    public bool reach;

    void Start()
    {
        reach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        reach=true;
        Pegar.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        reach=false;
        Pegar.enabled = false;
    }

    void Update()
    {
        if(inputController.PegarItem()&&reach)
        {

            //Time.timeScale = 0.0f;
            Keypad.enabled = true;

        }
    }
}
