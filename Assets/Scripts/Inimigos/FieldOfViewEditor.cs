using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Vector3 position = new Vector3(fov.transform.position.x, fov.transform.position.y + 0.5f, fov.transform.position.z);
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(position,position + viewAngle01 * fov.radius);
        Handles.DrawLine(position,position + viewAngle02 * fov.radius);

        //Vector3 viewAngle03 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 4);
        //Vector3 viewAngle04 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 4);

        //Handles.color = Color.yellow;
        //Handles.DrawLine(position, position + viewAngle03 * fov.radius);
        //Handles.DrawLine(position, position + viewAngle04 * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Vector3 player_position = new Vector3(fov.playerRef.transform.position.x, fov.playerRef.transform.position.y + 0.5f, fov.playerRef.transform.position.z);
            Handles.DrawLine(position, player_position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
