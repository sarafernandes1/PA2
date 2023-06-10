using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPostProcessing : MonoBehaviour
{
    public Material[] material;
    public Camera[] outras_cameras;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AtivarPostProcessing();
    }

    void AtivarPostProcessing()
    {
        int pp = PlayerPrefs.GetInt("PP");
        


        switch (pp)
        {
            //Inferno
            case 1:
                
                Camera.main.GetComponent<blip>().enabled = true;

                Camera.main.GetComponent<blip>().mat = material[1];
                outrasCameras(1, true);
                break;
            //Night Vision
            case 2:
                Camera.main.GetComponent<blip>().enabled = false;
                Camera.main.GetComponent<NightVisionTint>().enabled = true;
                outrasCameras(2, true);

                break;
            //Obra Dinn
            case 3:
                Camera.main.GetComponent<blip>().enabled = false;
                Camera.main.GetComponent<ObraDinnShader>().enabled = true;
                outrasCameras(3, true);

                break;
            //VHS
            case 4:
                Camera.main.GetComponent<blip>().enabled = true;

                Camera.main.GetComponent<blip>().mat = material[0];
                outrasCameras(4, true);

                break;
            default:
                break;
        }

        if(PlayerPrefs.GetInt("VHS")==1 && PlayerPrefs.GetInt("I") == 1)
        {
            Camera.main.GetComponent<blip>().enabled = false;
        }

        if (PlayerPrefs.GetInt("N") == 1)
        {
            Camera.main.GetComponent<NightVisionTint>().enabled = false;
        }

        if (PlayerPrefs.GetInt("O") == 1)
        {
            Camera.main.GetComponent<ObraDinnShader>().enabled = false;
        }
    }


    void outrasCameras(int index, bool resultado)
    {
        foreach (var c in outras_cameras)
        {
            switch (index)
            {
                case 1:
                    c.GetComponent<blip>().enabled = resultado;
                    c.GetComponent<blip>().mat = material[1];
                    break;
                case 2:
                    c.GetComponent<blip>().enabled = false;
                    c.GetComponent<NightVisionTint>().enabled = resultado;
                    break;
                case 3:
                    c.GetComponent<blip>().enabled = false;
                    c.GetComponent<ObraDinnShader>().enabled = resultado;
                    break;
                case 4:
                    c.GetComponent<blip>().enabled = resultado;
                    c.GetComponent<blip>().mat = material[0];
                    break;
                default:
                    break;
            }
        }
    }
}
