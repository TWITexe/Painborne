using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [SerializeField] private float shortShake = 0.3f;
    [SerializeField] private float longShake = 0.8f;

    private Tween currentTween;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
                player.GetComponent<Health>().TakeDamage(30);
    }

    public Tween Attack(bool longDelay)
    {
        // Прерываем прошлое движение, если есть
        currentTween?.Kill();

        Sequence seq = DOTween.Sequence();

        // Опускаем вниз
        seq.Append(transform.DOMove(bottomPoint.position, moveDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            float strenght = longDelay ? longShake : shortShake;

            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            if (impulseSource != null)
                impulseSource.GenerateImpulse(randomDir * strenght);

        }));


        // Ждём 1 или 4 секунды
        seq.AppendInterval(longDelay ? 2f : 1f);

        // Поднимаем обратно
        seq.Append(transform.DOMove(topPoint.position, moveDuration).SetEase(Ease.OutQuad));

        return seq;
    }

    
}
