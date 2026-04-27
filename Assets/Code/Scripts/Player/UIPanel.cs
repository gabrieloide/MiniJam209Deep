using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;

    public void Show(System.Action onComplete = null)
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, duration).SetEase(showEase).SetUpdate(true).OnComplete(() => onComplete?.Invoke());
    }

    public void Hide(System.Action onComplete = null)
    {
        transform.DOScale(0, duration).SetEase(hideEase).SetUpdate(true).OnComplete(() => {
            gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    public void InstantShow()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
    }
}
