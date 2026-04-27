using UnityEngine;
using DG.Tweening;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    [SerializeField] private float delay = 2f;

    [SerializeField] private Transform parentWaypoints;
    [SerializeField] private Ease tween;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        waypoints = parentWaypoints.GetComponentsInChildren<Transform>();
        Debug.Log("Waypoints: " + waypoints.Length);
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to HazardSaw");
            enabled = false;
        }
        StartMoving();
    }

    void StartMoving()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        transform.DOMove(waypoints[currentWaypointIndex].position, duration)
            .SetEase(tween).SetDelay(delay)
            .OnComplete(StartMoving);
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawWireSphere(waypoints[i].position, 0.1f);
            if (i < waypoints.Length - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
