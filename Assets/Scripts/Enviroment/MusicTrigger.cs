using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool stopMusic = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (audioSource != null && collision.GetComponent<PlayerMoveController>() != null)
        {
            if (stopMusic)
            {
                MusicManager.Instance.StopMusic();
                return;
            }
            else if (audioSource.clip == musicClip && audioSource.isPlaying)
            {
                Debug.Log("Tis Music is already playing.");
                return;
            }
               
            if (audioSource.isPlaying)
                MusicManager.Instance.StopMusic();

            MusicManager.Instance.PlayMusic(musicClip);
        }
    }
}
