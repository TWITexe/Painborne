using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    [SerializeField] private Clues clue;

    [SerializeField] private GameObject moveClue;
    [SerializeField] private GameObject jumpClue;
    [SerializeField] private GameObject downClue;
    [SerializeField] private GameObject healthClue;
    [SerializeField] private GameObject doorClue;

    [SerializeField] private float fadeDuration = 0.3f;

    private Dictionary<GameObject, Coroutine> fadeCoroutines = new();

    private void Awake()
    {
        HideImmediately(moveClue);
        HideImmediately(jumpClue);
        HideImmediately(downClue);
        HideImmediately(healthClue);
        HideImmediately(doorClue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Clue triggerClue))
        {
            clue = triggerClue.ClueType;
            ClueView(clue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Clue triggerClue))
        {
            clue = Clues.None;
            ClueView(clue);
        }
    }

    private void ClueView(Clues activeClue)
    {
        switch (activeClue)
        {
            case Clues.Move:
                ClueActivate(true, false, false, false, false);
                break;

            case Clues.Jump:
                ClueActivate(false, true, false, false, false);
                break;

            case Clues.Down:
                ClueActivate(false, false, true, false, false);
                break;

            case Clues.Health:
                ClueActivate(false, false, false, true, false);
                break;

            case Clues.Door:
                ClueActivate(false, false, false, false, true);
                break;

            case Clues.None:
                ClueActivate(false, false, false, false, false);
                break;
        }
    }

    private void ClueActivate(bool move, bool jump, bool down, bool health, bool door)
    {
        SetClueState(moveClue, move);
        SetClueState(jumpClue, jump);
        SetClueState(downClue, down);
        SetClueState(healthClue, health);
        SetClueState(doorClue, door);
    }

    private void SetClueState(GameObject clueObject, bool show)
    {
        if (fadeCoroutines.TryGetValue(clueObject, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
        }

        fadeCoroutines[clueObject] = StartCoroutine(
            FadeClue(clueObject, show)
        );
    }

    private IEnumerator FadeClue(GameObject clueObject, bool show)
    {
        SpriteRenderer spriteRenderer =
            clueObject.GetComponentInChildren<SpriteRenderer>(true);

        if (spriteRenderer == null)
        {
            yield break;
        }

        if (show)
        {
            clueObject.SetActive(true);
        }

        Color color = spriteRenderer.color;

        float startAlpha = color.a;
        float targetAlpha = show ? 1f : 0f;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float t = timer / fadeDuration;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            spriteRenderer.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        spriteRenderer.color = color;

        if (!show)
        {
            clueObject.SetActive(false);
        }

        fadeCoroutines.Remove(clueObject);
    }

    private void HideImmediately(GameObject clueObject)
    {
        SpriteRenderer spriteRenderer =
            clueObject.GetComponentInChildren<SpriteRenderer>(true);

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }

        clueObject.SetActive(false);
    }
}

public enum Clues
{
    Move,
    Jump,
    Down,
    Health,
    Door,
    None
}