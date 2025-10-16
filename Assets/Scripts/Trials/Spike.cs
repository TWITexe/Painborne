using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Collections;

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
        currentTween?.Kill();
        Sequence seq = DOTween.Sequence();
        
        seq.Append(transform.DOMove(bottomPoint.position, moveDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                float strength = longDelay ? longShake : shortShake;
                Vector3 randomDir = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    0f
                ).normalized;

                if (impulseSource != null)
                    impulseSource.GenerateImpulse(randomDir * strength);
            })
        );

        seq.AppendInterval(longDelay ? 2f : 1f);

        seq.Append(transform.DOMove(topPoint.position, moveDuration)
            .SetEase(Ease.OutQuad)
        );

        return seq;
    }


}
