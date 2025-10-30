using UnityEngine;

public class HealthMonument : MonoBehaviour, IInteractable
{
    private Health playerForInteraction;
    [SerializeField] private bool disableColliderAfterUse;


    public void Interact()
    {
        playerForInteraction.Heal(100);
        if (disableColliderAfterUse)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {
            playerForInteraction = player.GetComponent<Health>();           
        }

    }
}
