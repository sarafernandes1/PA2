using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextNotes : MonoBehaviour
{
    public Canvas note;
    public GameObject input;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Exit()
    {
        note.enabled = false;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (note.enabled)
        {
            //input.GetComponent<InputController>().enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
