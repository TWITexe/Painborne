using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Range(0,3)]
    [SerializeField] private int leverIndex;
    [SerializeField] private PuzzleManager puzzleManager;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip openSfx;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        audioSource.PlayOneShot(openSfx);
        Debug.Log($"Рычаг {leverIndex} активирован!");
        animator.SetTrigger("IsPressed");
        puzzleManager.ActivateLever(leverIndex);
        
    }
}
