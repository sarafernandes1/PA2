using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSegurança : MonoBehaviour
{
    bool displayMessage = false;
    public GameObject holofote;
    float angle;
    public bool esquerda=false, direita = true;

    void Start()
    {
    }

    void Update()
    {
        if ( holofote.transform.rotation.y>=0.20 )
        {
            esquerda = true;
            direita = false;
        }
        if (holofote.transform.rotation.y <= -0.20)
        {
            esquerda = false;
            direita = true;
        }

        if (direita)
        {
            angle = 5.0f * Time.deltaTime;
        }
        else
        {
            angle = -5.0f * Time.deltaTime;
        }

        holofote.transform.Rotate(new Vector3(0.0f, angle, 0.0f));
       
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(0.5f);
        
    }

    private void OnTriggerStay(Collider other)
    {
        displayMessage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        displayMessage = false;

    }

    private void OnGUI()
    {
        if (displayMessage)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Jogador apanhado");
            GameOver.gameOver = true;
        }
    }
}
