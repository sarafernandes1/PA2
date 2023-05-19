using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MagicLightSource : MonoBehaviour
{
    public Material[] reveal;
    public Light light;
    public Camera camera;
    public int lightEstado = 0;
    public GameObject player;
    public bool holofote = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        //reveal.SetInt("_IsLightOn", 1);
        for (int i = 0; i < reveal.Length; i++)
        {
            
            reveal[i].SetVector("_LightPosition", player.transform.position);
            reveal[i].SetVector("_LightDirection", -camera.transform.forward);
            reveal[i].SetFloat("_LightAngle", 40);
        }

        if (holofote)
        {
            reveal[0].SetInt("_IsLightOn", 1);
            reveal[0].SetFloat("_distancia", 1.0f);
                   
            reveal[0].SetVector("_LightPosition", transform.position);
            reveal[0].SetVector("_LightDirection", -transform.forward);
            reveal[0].SetFloat("_LightAngle", 20);
        }
    }

    public void ligarLuz(int a )
    {
        for (int i = 0; i < reveal.Length; i++)
        {
            reveal[i].SetInt("_IsLightOn", a);
        }
    }
}