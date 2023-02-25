using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Keypad : MonoBehaviour
{

    public GameObject player;
    public GameObject keypad;
    public GameObject HUD;

    public Text text;
    public string answer = "0451";

    public GameObject animate;
    public Animator animator;

    public AudioSource button;
    public AudioSource wrong;
    public AudioSource correct;

    public bool doingAnimation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Number(int number)
    {
        text.text = number.ToString();
        button.Play();
    }

    public void Execute()
    {
        if(text.text == answer)
        {
            correct.Play();
            text.text = "Correct";
        }
        else
        {
            wrong.Play();
            text.text = "Wrong";
        }
    }

    public void Clear()
    {
        text.text = "";
        button.Play();
    }

    public void Exit()
    {
        keypad.SetActive(false);
        HUD.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == "Correct" && animate)
        {
            animator.SetBool("animate",true);
            Debug.Log("its open");
        }

        if (keypad.activeInHierarchy)
        {
            HUD.SetActive(false);
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
