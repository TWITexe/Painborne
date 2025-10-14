using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openHeight = 3f;
    [SerializeField] private float duration = 1.5f;
    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen) return;
        isOpen = true;

        transform.DOMoveY(transform.position.y + openHeight, duration)
                 .SetEase(Ease.OutQuad);
    }
}

