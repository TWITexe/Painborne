using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFading : MonoBehaviour
{
    
    [SerializeField] float minIntensity = 1.3f;
    [SerializeField] float maxIntensity = 2f;
    [SerializeField] float speed = 2f;

    private Light2D light2D;

    private float timer = 0f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }
    void Update()
    {
        
        timer += Time.deltaTime * speed;

        float t = Mathf.PingPong(timer, 1f);

        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
