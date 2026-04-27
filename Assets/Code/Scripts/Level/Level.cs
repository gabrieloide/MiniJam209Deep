using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform center;
    public Vector3 CenterPosition => center != null ? center.position : transform.position;
    public Transform CenterTransform => center;

    private void OnDrawGizmos()
    {
        if (center != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(center.position, new Vector3(18, 10, 0)); // Slightly larger than typical 16:9
            Gizmos.DrawSphere(center.position, 0.5f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
