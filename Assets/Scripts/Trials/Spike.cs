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
        // ��������� ������� ��������, ���� ����
        currentTween?.Kill();

        Sequence seq = DOTween.Sequence();

        // �������� ����
        seq.Append(transform.DOMove(bottomPoint.position, moveDuration).SetEase(Ease.InQuad));

        // ��� 1 ��� 4 �������
        seq.AppendInterval(longDelay ? 4f : 1f);

        // ��������� �������
        seq.Append(transform.DOMove(topPoint.position, moveDuration).SetEase(Ease.OutQuad));

        return seq;
    }
}
