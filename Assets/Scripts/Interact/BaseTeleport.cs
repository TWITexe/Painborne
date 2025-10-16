using UnityEngine;

public class BaseTeleport : MonoBehaviour, IInteractable
{
    [SerializeField] Transform teleportationPoint;
    [SerializeField] Door door;
    private GameObject playerForTeleportation;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
    public void Interact()
    {
        Debug.Log("teleport");
        if (door.IsOpen)
            playerForTeleportation.transform.position = teleportationPoint.position;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {
            Debug.Log("trigger");
            playerForTeleportation = player.gameObject;
        }
            
    }
    
}
