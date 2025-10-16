using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openDistance= 3f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private bool onLock = false; // требуется ли ключ для открытия
    private bool isOpen = false; // уже открыт
    public bool IsOpen => isOpen;

    public void Interact()
    {
        if (isOpen || onLock) return;
        isOpen = true;

        transform.DOMoveY(transform.position.y + openDistance, duration)
                 .SetEase(Ease.OutQuad);
    }
    public void Unlock()
    {
        if (onLock)
            onLock = false;
    }
}

