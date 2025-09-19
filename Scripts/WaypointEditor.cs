using UnityEditor;
using UnityEngine;
using UnityEditor;
[InitializeOnLoad()]
public class WaypointEditor 
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {

            Gizmos.color = Color.blue;

        }
        else
        {
            Gizmos.color = Color.red;
        }


        Gizmos.DrawSphere(waypoint.transform.position, 0.5f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(



             waypoint.transform.position + (waypoint.transform.right * waypoint.waypointwidth / 2f),
              waypoint.transform.position - (waypoint.transform.right * waypoint.waypointwidth / 2f)

        );

        if (waypoint.PreviousWaypoint != null) { 
        
        Gizmos.color = Color.yellow;

            Vector3 offset = waypoint.transform.right * waypoint.waypointwidth / 2f;
            Vector3 offsetTo = waypoint.PreviousWaypoint.transform.right * waypoint.PreviousWaypoint.waypointwidth / 2f;

            Gizmos.DrawLine(

                waypoint.transform.position + offset,
                waypoint.PreviousWaypoint.transform.position + offsetTo
                );
        
        
        }

        if (waypoint.NextWaypoint != null)
        {

            Gizmos.color = Color.green;

            Vector3 offset = waypoint.transform.right * -waypoint.waypointwidth / 2f;
            Vector3 offsetTo = waypoint.NextWaypoint.transform.right * -waypoint.NextWaypoint.waypointwidth / 2f;

            Gizmos.DrawLine(

                waypoint.transform.position + offset,
                waypoint.NextWaypoint.transform.position + offsetTo
                );


        }

    }



}
