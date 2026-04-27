using UnityEngine;

public class PlayerDepthTracker : MonoBehaviour
{
    private float initialY;
    private float maxDepth;

    private void Start()
    {
        initialY = transform.position.y;
        maxDepth = initialY;
    }

    private void Update()
    {
        if (GameManager.Instance.isDead || !GameManager.Instance.canPlay) return;

        if (transform.position.y < maxDepth)
        {
            maxDepth = transform.position.y;
            GameManager.Instance.depth = maxDepth;
        }
    }
}
