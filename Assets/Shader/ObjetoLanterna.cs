using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoLanterna : MonoBehaviour
{
    public Transform player;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        //gameObject.GetComponent<Renderer>().material.SetFloat("_distanceA", distance);
    }
}
