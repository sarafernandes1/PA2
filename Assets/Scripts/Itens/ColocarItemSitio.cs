using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarItemSitio : MonoBehaviour
{
    public InputController inputController;
    public GameObject porta;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (inputController.PegarItem() && GameObject.Find("Cube (1)")!=null)
        {
            GameObject.Find("Cube (1)").transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y+0.5f, transform.position.z);
            GameObject.Find("Cube (1)").transform.SetParent(this.transform);
            Destroy(porta.gameObject);
        }
    }
}
