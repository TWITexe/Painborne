using UnityEngine;

public class BaseTeleport : MonoBehaviour, IInteractable
{
    [SerializeField] Transform teleportationPoint;
    private GameObject playerForTeleportation;

    public void Interact()
    {
        if (gameObject.GetComponent<Door>().IsOpen)
            playerForTeleportation.transform.position = teleportationPoint.position;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
            playerForTeleportation = player.gameObject;
    }
    
}
