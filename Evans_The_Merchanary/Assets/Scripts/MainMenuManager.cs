using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sound Properties")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        Time.timeScale = 0f;
        SFXManager.instance.PlaySound(backgroundMusic, transform.position);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnGameStarted()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

}
