using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.Progress;

public class OpenCanvas : MonoBehaviour
{
    public Canvas Pegar, UICanvas;
    public InputController inputController;
    public Camera MainCamera;
    public bool reach;

    void Start()
    {
        reach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            reach = true;
            Pegar.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            reach = false;
            Pegar.enabled = false;
        }
    }

    void Update()
    {
        if(inputController.PegarItem()&&reach)
        {

            Time.timeScale = 0.0f;
            UICanvas.enabled = true;

        }
    }
}
