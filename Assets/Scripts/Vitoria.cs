using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitoria : MonoBehaviour
{
    public static bool artefacto;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (artefacto)
        {

            canvas.enabled = true;
            StartCoroutine(espera());
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0.0f;
    }
}
