using UnityEngine;
using System.Collections;

public class PainfulActions : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = new Color(1f, 0.2f, 0.2f);
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 2;
    private Color originalColor;

    private Material originalMaterial;
    void Awake()
    {
        playerHealth = gameObject.GetComponent<Health>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
            playerHealth.TakeDamage(5);

    }
    

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnDamaged += HandleDamage;
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnDamaged -= HandleDamage;
    }

    private void HandleDamage(int currentHealth)
    {
        Flash();
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.material.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

}
