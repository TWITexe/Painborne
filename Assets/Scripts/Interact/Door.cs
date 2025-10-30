using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openDistance = 3f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private bool onLock = false; // требуется ли ключ для открытия
    [SerializeField] private bool cameraShake = false;
    private bool isOpen = false; // уже открыт
    public bool IsOpen => isOpen;

    [Header("If camera shake = true")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    public void Interact()
    {
        if (isOpen || onLock) return;
        isOpen = true;

        if (cameraShake)
            StartImpulse();

        transform.DOMoveY(transform.position.y + openDistance, duration)
                 .SetEase(Ease.OutQuad);
    }
    public void Unlock()
    {
        if (onLock)
            onLock = false;
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

