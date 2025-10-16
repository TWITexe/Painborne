using UnityEngine;

public class HealthMonument : MonoBehaviour, IInteractable
{
    private Health playerForInteraction;
    public void Interact()
    {
        playerForInteraction.Heal(100);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {          
            playerForInteraction = player.GetComponent<Health>();
        }

    }
}
