using UnityEngine;

public class PlayerInteraction2D : MonoBehaviour
{
    private IInteractable currentInteractable;
    [SerializeField] KeyCode keyForIntractable;

    private void Update()
    {
        if (Input.GetKeyDown(keyForIntractable) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable _))
        {
            currentInteractable = null;
        }
    }
}

