using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codigoCerto : MonoBehaviour
{
    void ligarLuz()
    {
        GetComponent<Light>().enabled = true;
    }

    void ligarLuz2()
    {
        GetComponent<Light>().enabled = true;
    }

    //void anoitecer()
    //{
    //    transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    //}
}