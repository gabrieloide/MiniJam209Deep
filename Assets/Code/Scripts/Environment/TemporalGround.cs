using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TemporalGround : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.5f;
    private bool isTriggered = false;

    public void OnDissapearGround()
    {
        if (isTriggered) return;
        isTriggered = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.crumble);
        transform.DOShakePosition(lifeTime, 0.05f, 20, 90, false, true).SetUpdate(true);
        StartCoroutine(DissapearRoutine());
    }

    private IEnumerator DissapearRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        TogglePlatform(false);
        yield return new WaitForSeconds(lifeTime);
        isTriggered = false;
        TogglePlatform(true);
    }

    private void TogglePlatform(bool state)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers) r.enabled = state;
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var c in colliders) c.enabled = state;
    }
}