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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //reveal.SetInt("_IsLightOn", 1);
        for (int i = 0; i < reveal.Length; i++)
        {
            reveal[i].SetVector("_LightPosition", player.transform.position);
            reveal[i].SetVector("_LightDirection", -camera.transform.forward);
            reveal[i].SetFloat("_LightAngle", 40);
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