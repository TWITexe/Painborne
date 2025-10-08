using UnityEngine;

public class ObstacleOverlap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMoveController>() != null)
            spriteRenderer.sortingOrder -= 2;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMoveController>() != null)
            spriteRenderer.sortingOrder += 2;
    }
}