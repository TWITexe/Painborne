using UnityEngine;

public class HealthMonument : MonoBehaviour, IInteractable
{
    private Health playerForInteraction;

    [SerializeField] private bool disableColliderAfterUse;
    [SerializeField] private Transform spawnPoint;

    public void Interact()
    {
        playerForInteraction.Heal(100);

        SaveSystem.Instance.SavePoint(spawnPoint.position);

        if (disableColliderAfterUse)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {
            playerForInteraction = player.GetComponent<Health>();
        }
    }
}