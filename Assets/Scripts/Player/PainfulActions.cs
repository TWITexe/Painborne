using UnityEngine;

public class PainfulActions : MonoBehaviour
{
    private Health playerHealth;
    void Awake()
    {
        playerHealth = gameObject.GetComponent<Health>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
            playerHealth.TakeDamage(5);

    }
}
