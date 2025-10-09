using UnityEngine;
using DG.Tweening;

public class Spike : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float moveDuration = 0.5f;

    private Tween currentTween;

    public Tween Attack(bool longDelay)
    {
        // Прерываем прошлое движение, если есть
        currentTween?.Kill();

        Sequence seq = DOTween.Sequence();

        // Опускаем вниз
        seq.Append(transform.DOMove(bottomPoint.position, moveDuration).SetEase(Ease.InQuad));

        // Ждём 1 или 4 секунды
        seq.AppendInterval(longDelay ? 4f : 1f);

        // Поднимаем обратно
        seq.Append(transform.DOMove(topPoint.position, moveDuration).SetEase(Ease.OutQuad));

        return seq;
    }
}
