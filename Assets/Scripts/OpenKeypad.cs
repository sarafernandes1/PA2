using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeypad : MonoBehaviour
{
    public GameObject keypad;
    public GameObject keypadText;
    public InputController inputController;

    public bool inReach;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach=true;
            keypadText.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inReach = false;
        keypadText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Interact") && inReach)
        //{
        //    keypad.SetActive(true);
        //}

        if (inputController.PegarItem() && inReach)
        {
            keypad.SetActive(true);
        }
    }
}
