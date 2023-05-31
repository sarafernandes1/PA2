using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dica : MonoBehaviour
{
    public Canvas canvas;
    public Image disfarce_image;
    float time = 20;
    private InputController inputController;
    bool isinArea = false;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isinArea && inputController.Pausa())
        {
            MenuManager.Pausa = false;
            canvas.enabled = false;
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
       }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        isinArea = true;
        canvas.enabled = true;
        Time.timeScale = 0.0f;
    }

   

    public void ButtonSair()
    {
        canvas.enabled = false;

        canvas.gameObject.SetActive( false);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
