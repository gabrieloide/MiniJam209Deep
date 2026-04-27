using UnityEngine;

public class Hazard : MonoBehaviour
{
    private string hazardTag = "Hazard";

    private void Awake()
    {
        if (gameObject.tag != hazardTag)
        {
            Debug.Log("Hazard");
            gameObject.tag = hazardTag;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 deathPosition = collision.transform.position;
            Debug.Log("Player hit hazard");
            GameManager.Instance.OnDeath(deathPosition, gameObject.name);
        }
    }
}