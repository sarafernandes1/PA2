using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspelhosPuzzle : MonoBehaviour
{
	public int reflections;
	public float maxLength;

	private LineRenderer lineRenderer;
	private Ray ray;
	private RaycastHit hit;
	public Transform primeiroEspelho;

	public GameObject[] espelhos;
	public GameObject objeto;

	public GameObject ultimoEspelho;
	public bool completo = false;
	public bool Venus = false;
	public Animator animator;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		if (!completo)
		{
			ray = new Ray(transform.position, transform.forward);

			lineRenderer.positionCount = 1;
			lineRenderer.SetPosition(0, transform.position);
			float remainingLength = maxLength;

			for (int i = 0; i < reflections; i++)
			{
				if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
                {
                    if (hit.collider.tag != "Mirror")
                    {
                        break;
                    }
                    lineRenderer.positionCount += 1;

					lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

					remainingLength -= Vector3.Distance(ray.origin, hit.point);
					ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

					float distance = Vector3.Distance(hit.point, objeto.transform.position);
					if (distance <= 0.5f)
					{
						foreach (var espelho in espelhos)
						{
							espelho.GetComponent<MoverEspelhos>().bloqueado = true;
						}
					}



				}
				else
				{
						lineRenderer.positionCount += 1;
						lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
					
				}
			}
		}
		if(Vector3.Distance(lineRenderer.GetPosition(lineRenderer.positionCount-1), ultimoEspelho.transform.position) <= 3.0f)
        {
			if (!Venus)
			{
				ray = new Ray(ultimoEspelho.transform.position, objeto.transform.position);
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, objeto.transform.position);
				completo = true;
			}
            else
            {
				completo = true;
				animator.SetBool("Open", true);
			}
        }
	}
}
