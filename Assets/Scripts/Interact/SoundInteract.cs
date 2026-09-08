using UnityEngine;

public class SoundInteract : MonoBehaviour, IInteractable
{

    [SerializeField] AudioClip sfx;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        audioSource.PlayOneShot(sfx);
    }
}
