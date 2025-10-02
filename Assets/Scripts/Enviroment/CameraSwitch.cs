using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject zoomCamera;
    private bool isZooming = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Zoom();
            isZooming = !isZooming;
        }
    }
    private void Zoom()
    {
        if (isZooming)
            zoomCamera.SetActive(false);
        else
            zoomCamera.SetActive(true);
    }
}
