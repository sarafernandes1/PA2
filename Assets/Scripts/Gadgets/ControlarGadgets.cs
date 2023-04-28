using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarGadgets : MonoBehaviour
{
    public InputController inputController;
    public bool luz = false, disfarce = false, gancho = false;
    public GameObject luzObject;
    public Text tempo;
    public Text disfarceTempo;
    public RawImage image;
    int tLuz = 0;
    public Canvas disfarceCanvas;
    int tGancho = 0;

    public Image luzImage, disfarceImage, ganchoImage;
    float time = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        //  ATIVAR OS GADGETS
        if (inputController.ItemMao() == 0 && (tempo.text == "0" || tempo.text == "") &&
            !gameObject.GetComponent<PlayerController>().gancho)  // PARA SABER SE O DISFARCE N�O ESTA ATIVO
        {
            luz = !luz;
            disfarce = false;
            gancho = false;
            image.enabled = false;
            tLuz += 1;              // SABER NUMERO DE VEZES QUE BOTAO FOI PERMIDO PARA DEPOIS � SEGUNDA O DESATIVAR
            tGancho = 0;
        }
        else if (inputController.ItemMao() == 1 &&
            !gameObject.GetComponent<PlayerController>().gancho && time<=0)
        {
            time = 20;
            disfarce = !disfarce;
            luz = false;
            gancho = false;
            image.enabled = false;
            tGancho = 0;
            tLuz = 0;
        }
        else if (inputController.ItemMao() == 2 && (tempo.text == "0" || tempo.text == "") &&
            !gameObject.GetComponent<PlayerController>().gancho)
        {
            gancho = !gancho;
            disfarce = false;
            luz = false;
            tGancho += 1;
            tLuz = 0;

        }
        else
        {
            luz = false;
            disfarce = false;
            gancho = false;
        }

        //ATIVAR OS GADGETS
        if (luz)
        {
            luzImage.color = Color.red;
            ganchoImage.color = Color.white;
            disfarceImage.color = Color.white;

            luzObject.gameObject.GetComponent<Light>().enabled = true;
            luzObject.GetComponent<MagicLightSource>().ligarLuz(1);
            gameObject.GetComponent<GrappingHook>().enabled = false;
            gameObject.GetComponent<Disfarce>().ativo = false;
        }
        else if (disfarce)
        {
            //disfarceImage.color = Color.red;
            luzImage.color = Color.white;
            ganchoImage.color = Color.white;

            gameObject.GetComponent<Disfarce>().ativo = true;
            luzObject.gameObject.GetComponent<Light>().enabled = false;
            luzObject.GetComponent<MagicLightSource>().ligarLuz(0);
            gameObject.GetComponent<GrappingHook>().enabled = false;
        }
        else if (gancho)
        {
            ganchoImage.color = Color.red;
            luzImage.color = Color.white;
            disfarceImage.color = Color.white;

            gameObject.GetComponent<GrappingHook>().enabled = true;
            luzObject.gameObject.GetComponent<Light>().enabled = false;
            luzObject.GetComponent<MagicLightSource>().ligarLuz(0);
            gameObject.GetComponent<Disfarce>().ativo = false;
        }

        //QUANDO O BOTAO DE CADA UM � PREMIDO A SEGUNDA VE<
        if (luz && tLuz == 2)
        {
            luzImage.color = Color.white;
            luzObject.gameObject.GetComponent<Light>().enabled = false;
            luzObject.GetComponent<MagicLightSource>().ligarLuz(0);
            luz = false;
            tLuz = 0;
        }

        if (gancho && tGancho == 2 && !gameObject.GetComponent<PlayerController>().gancho)
        {
            ganchoImage.color = Color.white;
            image.enabled = false;
            gameObject.GetComponent<GrappingHook>().enabled = false;
            gancho = false;
            tGancho = 0;
        }

        //O DISFARCE NO SEU SCRIPT DESATIVA-SE
        if (!gameObject.GetComponent<Disfarce>().ativo)
        {
            disfarceImage.color = Color.white;
            disfarceCanvas.enabled = false;
            disfarce = false;
            time -= Time.deltaTime;
            int t = (int)time;
            disfarceTempo.text = t.ToString();
            if (time <= 0) disfarceTempo.text = "";
        }
    }
}
