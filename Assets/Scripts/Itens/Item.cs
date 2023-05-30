using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Canvas canvas, descricao;
    public Text nome, descricao_texto;
    public InputController inputController;
    public Camera camera;
    public ItensDados dados_item;
    GameObject player;

    public bool rotate, item_collected, item_ = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (item_collected && inputController.PegarItem())
        {
            transform.position = camera.transform.position + camera.transform.forward * 4.0f;
            rotate = true;
            canvas.enabled = false;
            Time.timeScale = 0.0f;
            item_ = false;
            item_collected = false;
        }

        if (rotate)
        {
            nome.text = dados_item.nome;
            descricao_texto.text = dados_item.description;
            canvas.enabled = false;
            descricao.enabled = true;
            RotateObject();
        }

    }

    void RotateObject()
    {
        Vector2 moviment = inputController.GetPlayerMoviment();
        float AD = inputController.Turning();

        if (AD < 0)
        {
            transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
        }
        if (AD > 0)
        {
            transform.Rotate(new Vector3(0.0f, -0.5f, 0.0f));
        }

        if (moviment.y < 0)
        {
            transform.Rotate(new Vector3(0.5f, 0.0f, 0.0f));
        }
        if (moviment.y > 0)
        {
            transform.Rotate(new Vector3(-0.5f, 0.0f, 0.0f));
        }


        StartCoroutine(espera());

        if (inputController.PegarItem() && item_)
        {

            player.GetComponent<Inventario>().ItemColetado(dados_item, gameObject);
            canvas.enabled = false;
            rotate = false;
            descricao.enabled = false;
            Time.timeScale = 1.0f;
            // item_ = false;
            // *****
            transform.rotation = initialRotation;

        }

        if (inputController.DescartarItem() && item_)
        {
            canvas.enabled = false;
            descricao.enabled = false;
            Time.timeScale = 1.0f;
            rotate = false;
            //item_ = false;

            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

    }

    IEnumerator espera()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        item_ = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.transform.parent == null) canvas.enabled = true;
            item_collected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.enabled = false;
            item_collected = false;
        }
    }
}