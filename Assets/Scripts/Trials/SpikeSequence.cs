using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

public class SpikeSequence : MonoBehaviour
{
    [SerializeField] private Spike spike1;
    [SerializeField] private Spike spike2;
    [SerializeField] private Spike spike3;

    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private Door door;
    [SerializeField] private GameObject lockWall;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject spikesCamera;

    [Header("Warning Shake")]
    [SerializeField] private float warningDuration = 1.5f;
    [SerializeField] private float warningStrength = 0.08f;
    [SerializeField] private int warningVibrato = 20;
    [SerializeField] private float warningRandomness = 90f;

    public void PlaySequence()
    {
        LockTheSpikeRoom();
        StartImpulse();

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(1f);
        seq.Append(WarnAndAttack(spike3, false));
        seq.Append(WarnAndAttack(spike2, false));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike1, false));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike1, true));
        seq.Join(WarnAndAttack(spike3, true));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike2, true));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike2, false));
        seq.Join(WarnAndAttack(spike3, true));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike1, false));
        seq.Join(WarnAndAttack(spike3, true));
        seq.AppendInterval(1f);

        seq.Append(WarnAndAttack(spike2, true));
        seq.Join(WarnAndAttack(spike3, false));
        seq.AppendInterval(3f);

        seq.Append(WarnAndAttack(spike1, false));
        seq.Join(WarnAndAttack(spike2, false));

        seq.OnComplete(() =>
        {
            if (door != null)
            {
                door.Unlock();
                door.Interact();
            }

            spikesCamera.SetActive(false);
            mainCamera.SetActive(true);
        });
    }

    private Sequence WarnAndAttack(Spike spike, bool pause)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(PlayWarningShake(spike));
        seq.Append(spike.Attack(pause));

        return seq;
    }

    private Tween PlayWarningShake(Spike spike)
    {
        Transform spikeTransform = spike.transform;
        Vector3 startLocalPos = spikeTransform.localPosition;

        spikeTransform.DOKill();

        return spikeTransform
            .DOShakePosition(
                duration: warningDuration,
                strength: warningStrength,
                vibrato: warningVibrato,
                randomness: warningRandomness,
                snapping: false,
                fadeOut: true)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                spikeTransform.localPosition = startLocalPos;
            });
    }

    private void LockTheSpikeRoom()
    {
        lockWall.transform.DOMoveY(transform.position.y - 7f, 1f)
            .SetEase(Ease.OutQuad);

        spikesCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    private void StartImpulse()
    {
        float strength = 0.4f;
        Vector3 randomDir = new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f),
            0f
        ).normalized;

        impulseSource.GenerateImpulse(randomDir * strength);
    }
}