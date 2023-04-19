using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MagicLightSource : MonoBehaviour
{
    public Material reveal;
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
        reveal.SetVector("_LightPosition", player.transform.position);
        reveal.SetVector("_LightDirection", -camera.transform.forward);
        reveal.SetFloat("_LightAngle",40);
    }

    public void ligarLuz(int a )
    {
        reveal.SetInt("_IsLightOn", a);
    }
}