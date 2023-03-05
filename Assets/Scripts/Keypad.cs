using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    //public int[] combination;

    //public GameObject num0;
    //public GameObject num1;
    //public GameObject num2;
    //public GameObject num3;
    //public GameObject num4;
    //public GameObject num5;
    //public GameObject num6;
    //public GameObject num7;
    //public GameObject num8;
    //public GameObject num9;

    //public GameObject clear;
    //public GameObject enter;

    //public GameObject text;

    //public string wait;
    //public string denied;
    //public string granted;

    //List<int> combo = new List<int>();
    //int index = 0;  

    //// Start is called before the first frame update
    //void Start()
    //{
    //    text.GetComponent<Text>().text = wait;
    //}

    //public void RightCombo(GameObject recived)
    //{
    //    if (recived == num0)
    //    {
    //        aumentarCombo(0);
    //    }

    //    if (recived == num1)
    //    {
    //        aumentarCombo(1);
    //    }

    //    if (recived == num2)
    //    {
    //        aumentarCombo(3);
    //    }

    //    if (recived == num3)
    //    {
    //        aumentarCombo(4);
    //    }

    //    if(recived == num4)
    //    {
    //        aumentarCombo(5);
    //    }

    //    if (recived == num6)
    //    {
    //        aumentarCombo(6);
    //    }

    //    if (recived == num6)
    //    {
    //        aumentarCombo(6);
    //    }

    //    if (recived == num7)
    //    {
    //        aumentarCombo(7);
    //    }

    //    if (recived == num8)
    //    {
    //        aumentarCombo(8);
    //    }

    //    if (recived == num9)
    //    {
    //        aumentarCombo(9);
    //    }

    //    if (recived == clear)
    //    {
    //        erase();
    //    }

    //    if (recived == enter)
    //    {
    //        analising();
    //    }
    //}

    //void analising()
    //{
    //    if (index==combination.Length)
    //    {
    //        bool correct = true;

    //        for(int i=0; i<combination.Length; i++)
    //        {
    //            if (combo[i] != combination[i])
    //            {
    //                correct = false;
    //                break;
    //            }
    //        }


    //        if (correct)
    //        {
    //            text.GetComponent<Text>().text = granted;

    //        }
    //        else
    //        {
    //            text.GetComponent<Text>().text = denied;

    //        }



    //    }
    //    else
    //    {
    //        text.GetComponent<Text>().text = denied;
    //        eraseWithoutChanging();
    //    }
    //}

    //void eraseWithoutChanging()
    //{
    //    combo.Clear();
    //    index = 0;

    //}

    //void erase()
    //{
    //    combo.Clear();
    //    index = 0;
    //    text.GetComponent<Text>().text = wait;
    //}



    //public void aumentarCombo(int num)
    //{
    //    combo.Add(num); 
    //    index++;
    //    text.GetComponent<Text>().text += "";

    //    for (int i = 0; i < index; i++)
    //    {
    //        text.GetComponent<Text>().text += "*";
    //    }
    //}

    public GameObject keypadObject;
    public Text text;
    public string combination = "0451";

    void Start()
    {
        //keypadObject.SetActive(false);

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

    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == "Granted")
        {
            Debug.Log("aberto");
        }

        if (keypadObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState= CursorLockMode.None;
        }
    }
}
