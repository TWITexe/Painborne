using UnityEngine;

public class PlayerSpawnPosition : MonoBehaviour
{
    private void Start()
    {
        if (!SaveSystem.Instance.HasSavePoint())
            return;

        Vector2 savePosition = SaveSystem.Instance.GetSavePoint();

        transform.position = savePosition;
    }
}