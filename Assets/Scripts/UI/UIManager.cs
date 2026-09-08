using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Death UI")]
    [SerializeField] private GameObject deadMenu;
    [SerializeField] private CanvasGroup panelCanvasGroup;

    [SerializeField] private CanvasGroup buttonsCanvasGroup;

    [SerializeField] private TMP_Text deathText;
    [SerializeField] private string[] deathMessages;

    [Header("Animation")]
    [SerializeField] private float panelFadeDuration = 1f;
    [SerializeField] private float buttonsFadeDuration = 0.5f;

    [SerializeField] private Health healthPlayer;

    private Coroutine deathCoroutine;

    public void ShowDeathMenu()
    {
        if (deathCoroutine != null)
        {
            StopCoroutine(deathCoroutine);
        }

        deathCoroutine = StartCoroutine(ShowDeathMenuCoroutine());
    }

    private IEnumerator ShowDeathMenuCoroutine()
    {
        deadMenu.SetActive(true);

        // выбираем случайный текст
        if (deathMessages.Length > 0)
        {
            int randomIndex = Random.Range(0, deathMessages.Length);
            deathText.text = deathMessages[randomIndex];
        }

        // yачальное состояние
        panelCanvasGroup.alpha = 0f;
        buttonsCanvasGroup.alpha = 0f;

        // gроявляем панель
        yield return StartCoroutine(FadeCanvasGroup(
            panelCanvasGroup,
            0f,
            1f,
            panelFadeDuration
        ));

        // после панели проявляем кнопки
        yield return StartCoroutine(FadeCanvasGroup(
            buttonsCanvasGroup,
            0f,
            1f,
            buttonsFadeDuration
        ));

        deathCoroutine = null;
    }

    private IEnumerator FadeCanvasGroup(
        CanvasGroup canvasGroup,
        float startAlpha,
        float targetAlpha,
        float duration)
    {
        float elapsed = 0f;

        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / duration;

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                progress
            );

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        healthPlayer.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        healthPlayer.OnDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        ShowDeathMenu();
    }
}