using UnityEngine;

public class SpikeAreaTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] SpikeSequence spikeSequence;
    private bool isActive = false;

    public void Interact()
    {
        if (isActive) return;
        isActive = true;

        spikeSequence.PlaySequence();
    }

}
