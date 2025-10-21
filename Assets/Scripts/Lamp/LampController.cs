using Unity.VisualScripting;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] private GameObject lightVisual;
    [SerializeField] private bool isOn;
    public bool IsOn => isOn;

    public void SetState(bool state)
    {
        isOn = state;
        if (lightVisual != null)
            lightVisual.SetActive(state);
    }

    public void Toggle()
    {
        SetState(!isOn);
    }
}
