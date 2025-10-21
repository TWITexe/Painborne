using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Range(0,2)]
    [SerializeField] private int leverIndex;
    [SerializeField] private PuzzleManager puzzleManager;

    public void Interact()
    {
        Debug.Log($"����� {leverIndex} �����������!");
        puzzleManager.ActivateLever(leverIndex);
    }
}
