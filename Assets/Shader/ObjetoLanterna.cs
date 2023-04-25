using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjetoLanterna : MonoBehaviour
{
    public GameObject player;
    public Material reveal;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(transform.position, player.transform.position);
         reveal.SetFloat("_distancia", distance);

    }
}
