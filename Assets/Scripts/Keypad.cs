using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public int[] combination;

    public GameObject num0;
    public GameObject num1;
    public GameObject num2;
    public GameObject num3;
    public GameObject num4;
    public GameObject num5;
    public GameObject num6;
    public GameObject num7;
    public GameObject num8;
    public GameObject num9;

    public GameObject clear;
    public GameObject enter;

    public GameObject text;

    public string wait;
    public string denied;
    public string granted;

    List<int> combo = new List<int>();
    int index = 0;  

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>().text = wait;
    }

    public void RightCombo(GameObject recived)
    {
        if (recived == num0)
        {
            aumentarCombo(0);
        }

        if (recived == num1)
        {
            aumentarCombo(1);
        }

        if (recived == num2)
        {
            aumentarCombo(3);
        }

        if (recived == num3)
        {
            aumentarCombo(4);
        }

        if(recived == num4)
        {
            aumentarCombo(5);
        }

        if (recived == num6)
        {
            aumentarCombo(6);
        }

        if (recived == num6)
        {
            aumentarCombo(6);
        }

        if (recived == num7)
        {
            aumentarCombo(7);
        }

        if (recived == num8)
        {
            aumentarCombo(8);
        }

        if (recived == num9)
        {
            aumentarCombo(9);
        }

        if (recived == clear)
        {

        }

        if (recived == enter)
        {

        }
    }



    public void aumentarCombo(int num)
    {
        combo.Add(num); 
        index++;
        text.GetComponent<Text>().text += "";

        for (int i = 0; i < index; i++)
        {
            text.GetComponent<Text>().text += "*";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
