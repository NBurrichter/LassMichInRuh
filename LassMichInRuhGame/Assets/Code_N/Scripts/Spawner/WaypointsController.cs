using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : MonoBehaviour
{
    public static List<Transform> waypoints;

    public void Awake()
    {
        waypoints = new List<Transform>();
    }

    public static void AddWayPoint(Transform waypoint)
    {
        waypoints.Add(waypoint);
    }
}
