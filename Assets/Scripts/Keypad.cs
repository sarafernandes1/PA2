using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    public GameObject input;
    public GameObject keypadObject;
    public Text text;
    public string combination = "0451";

    public GameObject animatedGameObject;
    public Animator animator;
    public bool animate;

    void Start()
    {

    }

    public void Number(int num)
    {
        text.text += num.ToString();
        
    }

    public void Enter()
    {
        if (text.text==combination)
        {
            text.text = "Granted";
        }
        else
        {
            text.text = "Denied";
        }
    }

    public void Clear()
    {
        text.text = "";
    }

    public void Exit()
    {
        keypadObject.SetActive(false);
        //input.GetComponent <InputController> ().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == "Granted" && animate)
        {
            animator.SetBool("Open", true); 
            Debug.Log("aberto");
        }

        if (keypadObject.activeInHierarchy)
        {
            //input.GetComponent<InputController>().enabled = true;
            Cursor.visible = true;
            Cursor.lockState= CursorLockMode.None;
        }
    }
}
