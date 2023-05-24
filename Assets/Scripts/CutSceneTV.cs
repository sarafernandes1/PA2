using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTV : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cameraPrincipal, cameraCutScene, player;
    public InputController inputController;
    bool isinArea = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputController.PegarItem() && isinArea)
        {
            cameraCutScene.SetActive(true);
            cameraPrincipal.SetActive(false);
            player.SetActive(false);
            StartCoroutine(cutScene());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isinArea = false;    
    }

    private void OnTriggerEnter(Collider other)
    {
        isinArea = true;
    }

    

    IEnumerator cutScene()
    {
        yield return new WaitForSeconds(5.0f);
        cameraPrincipal.SetActive(true);
        cameraCutScene.SetActive(false);
        player.SetActive(true);

    }
}
