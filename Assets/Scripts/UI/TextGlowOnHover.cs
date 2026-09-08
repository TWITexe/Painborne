using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextGlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    [SerializeField] Color normalColor;
    [SerializeField] Color pointColor;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = pointColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}
