using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    public Transform seeker;
    Vector3 targetPos;
    public GameObject winObject;

    public float speed = 20;

    [SerializeField] GameObject[] butoes;
    [HideInInspector] public bool isMovingObject;


    void Awake()
    {
        grid = GetComponent<Grid>();

        foreach (var item in butoes)
        {
            item.GetComponent<Butao>().pathfinding = this;
        }
    }

    void Update()
    {
        //FindPath(seeker.position, target.position);
        if (seeker.transform.position.y <= -100)
        {
            winObject.GetComponent<Light>().enabled = true;

            foreach (var item in butoes)
            {
                item.GetComponent<Butao>().dir = Butao.Direction.Win;
            }
        }
    }

    public void FindPath(Vector3 startPos, bool paraOrdenada, bool positive)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);

        targetPos = startPos;

        //Para restringir movimento, ao inves de alterar o Vector3 em si, alterar o Node. Para isso, usar esta l�gica onde define o proximo node vizinho

        if (paraOrdenada)
        {
            if (positive)
            {
                targetPos.x++;
            }
            else
            {
                targetPos.x--;
            }
        }
        else
        {
            if (positive)
            {
                targetPos.z++;
            }
            else
            {
                targetPos.z--;
            }
        }

        Node targetNode = grid.NodeFromWorldPoint(targetPos);



        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
                //foreach (var item in butoes)
                //{
                //    item.GetComponent<Butao>().dir = Butao.Direction.Win;
                //}
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }



    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

    public void MoveObject(bool x, bool direita_cima)
    {
        //if(Node)
        Debug.Log(seeker.position);
        FindPath(seeker.position, x, direita_cima);

        StartCoroutine(WaitForMovement());

        seeker.transform.position = Vector3.MoveTowards(seeker.position, new Vector3(targetPos.x, targetPos.y, targetPos.z), 1f); //* Time.deltaTime * 6f);
        //seeker.gameObject.GetComponent<Rigidbody>().MovePosition(targetPos);

        Debug.Log("Here!    " + seeker.position);

    }

    IEnumerator WaitForMovement()
    {
        isMovingObject = true;

        yield return new WaitForSeconds(1f);

        isMovingObject = false;
    }


}

