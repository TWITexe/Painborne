using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    [Tooltip("Целевое соотношение сторон (например, 16:9 = 1.7777)")]
    public float targetAspect = 16f / 9f;

    private Camera cam;
    private float lastAspect;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateCameraViewport();
    }

    private void Update()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        if (Mathf.Abs(currentAspect - lastAspect) > 0.001f)
        {
            UpdateCameraViewport();
        }
    }

    private void UpdateCameraViewport()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // узкий экран (например 4:3) - черные полосы сверху и снизу
            Rect rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            cam.rect = rect;
        }
        else
        {
            // широкий экран (например 21:9) - черные полосы по бокам
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            cam.rect = rect;
        }

        lastAspect = windowAspect;
    }
}
