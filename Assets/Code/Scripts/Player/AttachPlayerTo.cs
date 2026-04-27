using UnityEngine;
using System.Collections.Generic;

public class AttachPlayerTo : MonoBehaviour
{
    private List<Rigidbody2D> attachedBodies = new List<Rigidbody2D>();
    private Vector2 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 deltaPosition = (Vector2)transform.position - lastPosition;

        if (deltaPosition.magnitude > 0)
        {
            foreach (var rb in attachedBodies)
            {
                rb.position += deltaPosition;
            }
        }

        lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && !attachedBodies.Contains(rb)) attachedBodies.Add(rb);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) attachedBodies.Remove(rb);
        }
    }
}