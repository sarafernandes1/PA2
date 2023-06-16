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
    public GameObject capacete, olho, cartas, computador;
    public Camera[] cameras_secundarias;

    void Start()
    {
        
    }

    void Update()
    {
        VerificarObjetosEspeciais();
        toggleManager();

        if (isinArea && inputController.Pausa())
        {
            turnOff();
        }

        if (canvas.enabled)
        {
            
            //toggleManager();
            setPostProcessing();
        }
    }

    void VerificarObjetosEspeciais()
    {
        string helmet = PlayerPrefs.GetString("Helmet");
        string eye = PlayerPrefs.GetString("Eye");
        string ygo = PlayerPrefs.GetString("YGO");
        string computer = PlayerPrefs.GetString("Computer");


        if (helmet == "Coletado")
        {
            capacete.SetActive(true);
            toggle[2].interactable = true;
        }
        else
        {
            toggle[2].interactable = false;
        }

        if (eye == "Coletado")
        {
            olho.SetActive(true);
            toggle[3].interactable = true;
        }
        else
        {
            toggle[3].interactable = false;
        }

        if (ygo == "Coletado")
        {
            cartas.SetActive(true);
            toggle[1].interactable = true;
        }
        else
        {
            toggle[1].interactable = false;
        }

        if (computer == "Coletado")
        {
            computador.SetActive(true);
            toggle[0].interactable = true;
        }
        else
        {
            toggle[0].interactable = false;
        }
    }

    void setPostProcessing()
    {
        

        // VHS
        if (numero_botao == 4)
        {
            Camera.main.GetComponent<blip>().enabled = true;

            Camera.main.GetComponent<blip>().mat = material[0];
           
            PlayerPrefs.SetInt("PP", 4);
            outrasCameras(4, true);
        }

        //Inferno
        if (numero_botao == 1)
        {
            Camera.main.GetComponent<blip>().enabled = true;

            Camera.main.GetComponent<blip>().mat = material[1];
           
            PlayerPrefs.SetInt("PP", 1);
            outrasCameras(1, true);

        }

        //Night Vision
        if (numero_botao == 2)
        {
            Camera.main.GetComponent<blip>().enabled = false;
            Camera.main.GetComponent<NightVisionTint>().enabled = true;
           
            PlayerPrefs.SetInt("PP", 2);
            outrasCameras(2, true);

        }

        //Obra Dinn
        if (numero_botao == 3)
        {
            Camera.main.GetComponent<blip>().enabled = false;
            Camera.main.GetComponent<ObraDinnShader>().enabled = true;
           
            PlayerPrefs.SetInt("PP", 3);
            outrasCameras(3, true);

        }

       
    }

    void outrasCameras(int index, bool resultado)
    {
        foreach(var c in cameras_secundarias)
        {
            switch (index)
            {
                case 1:
                    c.GetComponent<blip>().enabled = resultado;
                    c.GetComponent<blip>().mat = material[1];
                    break;
                case 2:
                    c.GetComponent<blip>().enabled = false;
                    c.GetComponent<NightVisionTint>().enabled = resultado;
                    break;
                case 3:
                    c.GetComponent<blip>().enabled = false;
                    c.GetComponent<ObraDinnShader>().enabled = resultado;
                    break;
                case 4:
                    c.GetComponent<blip>().enabled = resultado;
                    c.GetComponent<blip>().mat = material[0];
                    break;
                default:
                    break;
            }
        }
    }

    void turnOff()
    {
        MenuManager.Pausa = false;
        canvas.enabled = false;
      //  canvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
       // gameObject.SetActive(false);
       
    }

    void toggleManager()
    {
        if(!toggle[0].isOn && !toggle[1].isOn)
        {
            Camera.main.GetComponent<blip>().enabled = false;
            outrasCameras(4, false);
        }

        if (!toggle[0].isOn)
        {
            PlayerPrefs.SetInt("VHS", 1);

        }
        else
        {
            PlayerPrefs.SetInt("VHS", 10);

        }

        if (!toggle[1].isOn)
        {
            PlayerPrefs.SetInt("I", 1);

        }
        else
        {
            PlayerPrefs.SetInt("I", 10);

        }

        if (!toggle[2].isOn)
        {
            PlayerPrefs.SetInt("N", 1);

        }
        else
        {
            PlayerPrefs.SetInt("N", 10);

        }

        if (!toggle[3].isOn)
        {
            PlayerPrefs.SetInt("O", 1);

        }
        else
        {
            PlayerPrefs.SetInt("0", 10);

        }

        if (!toggle[0].isOn && !toggle[1].isOn)
        {

            Camera.main.GetComponent<blip>().enabled = false;
            outrasCameras(1, false);
        }

        if (!toggle[2].isOn)
        {
            
            Camera.main.GetComponent<NightVisionTint>().enabled = false;
            outrasCameras(2, false);
        }

        if (!toggle[3].isOn)
        {
            
            Camera.main.GetComponent<ObraDinnShader>().enabled = false;
            outrasCameras(3, false);

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
