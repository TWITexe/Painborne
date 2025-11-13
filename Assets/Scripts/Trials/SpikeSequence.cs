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

    public void PlaySequence()
    {
        LockTheSpikeRoom();
        StartImpulse();
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(1f);
        seq.Append(spike3.Attack(false));
        seq.Append(spike2.Attack(false));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(false));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(true));
        seq.Join(spike3.Attack(true));
        seq.AppendInterval(1f);

        seq.Append(spike2.Attack(true));
        seq.AppendInterval(1f);
        seq.Append(spike2.Attack(false));
        seq.Join(spike3.Attack(true));
        seq.AppendInterval(1f);
        seq.Append(spike1.Attack(false));
        seq.Join(spike3.Attack(true));
        seq.AppendInterval(1f);
        seq.Append(spike2.Attack(true));
        seq.Join(spike3.Attack(false));
        seq.AppendInterval(3f);
        seq.Append(spike1.Attack(false));
        seq.Join(spike2.Attack(false));

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
    void LockTheSpikeRoom()
    {
        lockWall.gameObject.transform.DOMoveY(transform.position.y - 7, 1)
               .SetEase(Ease.OutQuad);
        spikesCamera.SetActive(true);
        mainCamera.SetActive(false);
    }
    void StartImpulse()
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

