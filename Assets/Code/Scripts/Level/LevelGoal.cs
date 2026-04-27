using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    private bool reached = false;
    [SerializeField] private bool isFinalGoal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (reached) return;

        if (collision.CompareTag("Player"))
        {
            reached = true;
            
            if (TryGetComponent<Collider2D>(out Collider2D col)) col.enabled = false;

            if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = Vector2.zero;
            }

            if (isFinalGoal)
            {
                GameManager.Instance.WinGame();
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.victory);
                LevelManager.Instance.NextLevel();
            }
        }
    }

    public void ResetGoal()
    {
        reached = false;
    }
}
