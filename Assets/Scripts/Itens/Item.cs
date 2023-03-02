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
    public Inventario inventario;

    bool rotate, item_collected, item_=false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if(item_collected && inputController.PegarItem())
        {
            transform.position = camera.transform.position + camera.transform.forward * 4.0f;
            rotate = true;
            Time.timeScale = 0.0f;
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

        if (moviment.x < 0)
        {
            transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
        }
        if (moviment.x > 0)
        {
            transform.Rotate(new Vector3(0.0f, -0.5f, 0.0f));
        }

        if (moviment.y < 0)
        {
            transform.Rotate(new Vector3(0.5f, 0.0f, 0.0f));
        }
        if (moviment.y> 0)
        {
            transform.Rotate(new Vector3(-0.5f, 0.0f, 0.0f));
        }


        StartCoroutine(espera());

        if (inputController.PegarItem() && item_ )
        {
            inventario.ItemColetado(dados_item,gameObject);
            gameObject.SetActive(false);
            descricao.enabled = false;
            // descricao.GetComponent<RawImage>().enabled = false;
            Time.timeScale = 1.0f;
            item_ = false;
        }

        if (inputController.DescartarItem() && item_)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            descricao.enabled = false;
            Time.timeScale = 1.0f;
            rotate = false;
            item_ = false;
        }

    }

    IEnumerator espera()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        item_ = true;
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true;
        item_collected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
        item_collected = false;
    }
}
