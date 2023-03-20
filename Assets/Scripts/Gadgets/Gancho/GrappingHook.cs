using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GrappingHook : MonoBehaviour
{
    public Camera camera;
    public LineRenderer line;
    public RawImage image;
    public Vector3 pos;
    public bool isHitingGancho = false;
    bool fly = false;
    bool preso = false;
    bool irPosGancho = false;

    public InputController inputController;
    Vector3 posicaoPlayer;

    void Start()
    {
    }

    void Update()
    {
            image.enabled = true;
            RaycastHit hit;
            Vector3 position = transform.position;
            position.y = 16.0f;
            Ray ray = new Ray(position, camera.transform.forward);
            //Debug.DrawRay(ray.origin, ray.direction, Color.blue);

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.collider.tag != "Player")
                {
                    if (hit.collider.tag == "Objeto")
                    {
                        isHitingGancho = true;
                        image.color = Color.green;

                        posicaoPlayer = hit.transform.position;
                    }
                    else
                    {
                        isHitingGancho = false;
                        image.color = Color.white;


                    }

                    if (!preso)
                    {
                        pos = hit.point;
                        pos.y = 16.0f;
                    }
                }
            }

            image.transform.position = camera.WorldToScreenPoint(pos);

            if (isHitingGancho && inputController.PegarItem())
            {
                irPosGancho = true;
            }

            if (irPosGancho)
            {
                setGancho();
            }

            if (inputController.GetPlayerJumpInThisFrame() && fly)
            {
                this.GetComponent<PlayerController>().gancho = false;
                fly = false;
            }

    }

    public void setGancho()
    {
        line.gameObject.SetActive(true);
        preso = true;
        fly = true;
        this.GetComponent<PlayerController>().gancho = true;

        if (fly)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, pos);
            transform.position = Vector3.MoveTowards(transform.position, posicaoPlayer, 10.0f * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, posicaoPlayer);

        if (distance <1.0f)
        {
            line.gameObject.SetActive(false);
            isHitingGancho = false;
            preso = false;
            irPosGancho = false;
        }
    }
}
