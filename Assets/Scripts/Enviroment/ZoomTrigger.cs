using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject cameraZoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {
            cameraZoom.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMoveController player))
        {
            cameraZoom.SetActive(false);
        }
    }

}
