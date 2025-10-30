using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Range(0,3)]
    [SerializeField] private int leverIndex;
    [SerializeField] private PuzzleManager puzzleManager;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        Debug.Log($"����� {leverIndex} �����������!");
        animator.SetTrigger("IsPressed");
        puzzleManager.ActivateLever(leverIndex);
    }
}
