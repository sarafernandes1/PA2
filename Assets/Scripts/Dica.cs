using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dica : MonoBehaviour
{
    public Canvas canvas;
    public Image disfarce_image;
    float time = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    public void ButtonSair()
    {
        canvas.enabled = false;

        canvas.gameObject.SetActive( false);
        Time.timeScale = 1.0f;
    }
}
