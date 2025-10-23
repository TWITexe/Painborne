using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Range(0,3)]
    [SerializeField] private int leverIndex;
    [SerializeField] private PuzzleManager puzzleManager;

    public void Interact()
    {
        Debug.Log($"Рычаг {leverIndex} активирован!");
        puzzleManager.ActivateLever(leverIndex);
    }
}
