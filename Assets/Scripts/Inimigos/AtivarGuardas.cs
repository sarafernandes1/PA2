using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarGuardas : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.GetComponent<Inventario>().temObjetoFinal)
        {
            Vitoria.artefacto=true;
            GameObject[] guardas = GameObject.FindGameObjectsWithTag("Guardas");
            foreach (var guarda in guardas)
            {
                guarda.GetComponent<FieldOfView>().batalhaFinal = true;
            }
        }
    }
}
