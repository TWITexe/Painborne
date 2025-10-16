using System.Collections;
using UnityEngine;

public class SpikeAreaTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] SpikeSequence spikeSequence;
    private bool isActive = false;

    public void Interact()
    {
        if (isActive) return;
        isActive = true;
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return null; 
        spikeSequence.PlaySequence();
    }
}
