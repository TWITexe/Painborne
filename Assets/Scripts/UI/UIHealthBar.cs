using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Health healthPlayer;
    [SerializeField] private Image hp;
    [SerializeField] private GameObject hpUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HideHealthBar();
        }
    }
    private void OnEnable()
    {
        healthPlayer.OnDamaged += UpdateHealthBar;
        healthPlayer.OnHealed += UpdateHealthBar;
        healthPlayer.OnDeath += HideHealthBar;
    }

    private void OnDisable()
    {
        healthPlayer.OnDamaged -= UpdateHealthBar;
        healthPlayer.OnHealed -= UpdateHealthBar;
        healthPlayer.OnDeath -= HideHealthBar;
    }

    private void UpdateHealthBar(int currentHP)
    {
        float fill = (float)currentHP / healthPlayer.MaxHealth;
        hp.fillAmount = fill;
    }

    private void HideHealthBar()
    {
        hpUI.SetActive(false);
    }
}
