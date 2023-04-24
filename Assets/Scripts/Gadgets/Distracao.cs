using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distracao : MonoBehaviour
{
    private Light luz;
    bool inArea = false;
    public InputController inputController;
    public Canvas canvas;

    void Start()
    {
        luz = GetComponent<Light>();
    }

    void Update()
    {
        if(inArea && inputController.PegarItem())
        {
            AudioManager.instance.Play("Alarme");
            luz.enabled = !luz.enabled;

        }

        if (luz.enabled)
        {
            ResponderSom();
        }
            


    }

    private void OnTriggerEnter(Collider other)
    {
        inArea = true;
        canvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;
        canvas.enabled = false;

    }

    void ResponderSom()
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guarda");
        foreach(var g in guards)
        {
            if(Vector3.Distance(transform.position, g.transform.position) <= luz.range)
            {
                if(g.GetComponent<InimigoParado>() != null)
                {
                    g.GetComponent<InimigoParado>().destraido = true;
                    g.GetComponent<InimigoParado>().posicao_destracao = transform.position;
                    g.GetComponent<InimigoParado>().objetoDistracao = luz.gameObject;

                   
                }
            }
        }
    }
}
