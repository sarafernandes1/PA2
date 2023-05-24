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
    public Transform mao;
    Vector3 posicaoPlayer;

    public float y;
    public float y1;

    public GameObject[] ganchos;
    public GameObject ganchomaisProximo = null;
    public Animator anim = null;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        image.enabled = true;
        RaycastHit hit;
        Vector3 position = transform.position;
        //if (position.y >= y - 4) position.y = y1;
        //else position.y = y;

        //  position.y += 8.0f;

        if(position.y != ganchomaisProximo.transform.position.y) position.y = ganchoProximo();

        
        Ray ray = new Ray(position, camera.transform.forward);
        

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Physics.Raycast(ray, out hit, 2000.0f))
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
                    pos.y = position.y;
                }
            }
        }

        image.transform.position = camera.WorldToScreenPoint(pos);

        if (isHitingGancho && inputController.PegarItem())
        {
            anim.SetFloat("Speed", 2.0f, 0.001f, Time.deltaTime);
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

        if (distance < 1.0f)
        {
            line.gameObject.SetActive(false);
            isHitingGancho = false;
            preso = false;
            irPosGancho = false;
        }
    }

    public float ganchoProximo()
    {
        float d, d_minima=10000;

            for (int i =0;i < ganchos.Length; i++)
        {
            d = Vector3.Distance(transform.position, ganchos[i].transform.position);
            if (d < d_minima )
            {
                d_minima = d;
                ganchomaisProximo = ganchos[i];
            }
        }

        return ganchomaisProximo.transform.position.y;
    }

}
