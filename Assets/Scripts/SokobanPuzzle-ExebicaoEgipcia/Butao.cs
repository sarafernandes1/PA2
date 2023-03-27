using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butao : MonoBehaviour
{
    public enum Direction
    {
        Up, Down, Left, Right, Win
    }

    public Direction dir;
    [HideInInspector]
    public Pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (pathfinding.isMovingObject)
        {
            return;
        }

        switch (dir)
        {
            case Direction.Up:
                pathfinding.MoveObject(false, true);
                break;
            case Direction.Down:
                pathfinding.MoveObject(false, false);
                break;
            case Direction.Left:
                pathfinding.MoveObject(true, false);
                break;
            case Direction.Right:
                pathfinding.MoveObject(true, true);
                break;
            case Direction.Win:
                break;
            default:
                break;
        }

    }


}