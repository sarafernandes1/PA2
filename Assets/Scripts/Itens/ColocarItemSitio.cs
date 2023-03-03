using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarItemSitio : MonoBehaviour
{
    public InputController inputController;
    public GameObject porta;
    public Canvas canvas;
    bool in_Area = false;
    bool coletado = false;

    void Start()
    {

    }

    void Update()
    {
        if (in_Area)
        {
            if (inputController.PegarItem())
            {
                GameObject.Find("Cube (2)").transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.5f, transform.position.z);
                GameObject.Find("Cube (2)").transform.SetParent(transform);
                GameObject.Find("Cube (2)").GetComponent<Item>().enabled = false;
                canvas.enabled = false;
                coletado = true;
                StartCoroutine(espera());
            }
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(porta.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameObject.Find("Cube (2)") != null && !coletado)
        {
            canvas.enabled = true;
            in_Area = true;
        }
        else
        {
            canvas.enabled = false;
            in_Area = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
        in_Area = false;
    }
}
