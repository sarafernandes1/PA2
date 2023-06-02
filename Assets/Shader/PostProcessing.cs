using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostProcessing : MonoBehaviour
{
    public Material[] material;
    public InputController inputController;
    public Canvas canvas;

    bool isinArea;
    int numero_botao;

    public Toggle[] toggle;
    bool toggle_ison = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isinArea && inputController.Pausa())
        {
            turnOff();
        }

        if (canvas.enabled)
        {
            toggleManager();
            setPostProcessing();
        }
    }

    void setPostProcessing()
    {
        // VHS
        if (numero_botao == 0)
        {
            Camera.main.GetComponent<blip>().enabled = true;

            Camera.main.GetComponent<blip>().mat = material[0];
        }

        //Inferno
        if (numero_botao == 1)
        {
            Camera.main.GetComponent<blip>().enabled = true;

            Camera.main.GetComponent<blip>().mat = material[1];
        }

        //Night Vision
        if (numero_botao == 2)
        {
            Camera.main.GetComponent<NightVisionTint>().enabled = true;

        }

        //Obra Dinn
        if (numero_botao == 3)
        {
            Camera.main.GetComponent<ObraDinnShader>().enabled = true;
        }
    }

    void turnOff()
    {
        MenuManager.Pausa = false;
        canvas.enabled = false;
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void toggleManager()
    {
        foreach(Toggle t in toggle)
        {
            if (t.isOn)
            {
                toggle_ison = true;
                continue;
            }
            if (toggle_ison)
            {
                t.interactable = false;
            }
        }
    }
    
    public void Botao(int numero)
    {
        numero_botao = numero;
    }

    private void OnTriggerEnter(Collider other)
    {
        isinArea = true;
        canvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    private void OnTriggerExit(Collider other)
    {
        isinArea = false;
    }

    public void ButtonSair()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;
    }
}
