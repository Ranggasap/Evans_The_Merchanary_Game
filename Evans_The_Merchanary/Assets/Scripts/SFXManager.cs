using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("SFX Settings")]
    [SerializeField] private AudioSource audioSource;

    public static SFXManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Vector2 targetPosition)
    {
        transform.position = targetPosition;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(AudioClip audioClip, Vector2 targetPosition)
    {
        transform.position = targetPosition;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
