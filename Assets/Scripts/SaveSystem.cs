using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private const string SaveX = "SavePoint_X";
    private const string SaveY = "SavePoint_Y";
    private const string HasSave = "HasSavePoint";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SavePoint(Vector2 position)
    {
        PlayerPrefs.SetFloat(SaveX, position.x);
        PlayerPrefs.SetFloat(SaveY, position.y);
        PlayerPrefs.SetInt(HasSave, 1);

        PlayerPrefs.Save();

        Debug.Log($"Checkpoint saved: {position}");
    }

    public bool HasSavePoint()
    {
        return PlayerPrefs.GetInt(HasSave, 0) == 1;
    }

    public Vector2 GetSavePoint()
    {
        return new Vector2(
            PlayerPrefs.GetFloat(SaveX),
            PlayerPrefs.GetFloat(SaveY)
        );
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SaveX);
        PlayerPrefs.DeleteKey(SaveY);
        PlayerPrefs.DeleteKey(HasSave);

        PlayerPrefs.Save();

        Debug.Log("Save deleted");
    }
}