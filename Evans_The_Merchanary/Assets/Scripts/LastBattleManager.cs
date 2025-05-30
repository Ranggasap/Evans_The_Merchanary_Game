using UnityEngine;
using UnityEngine.SceneManagement;

public class LastBattleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private bool hasWon = false;

    private void Update()
    {
        if (!hasWon && AllEnemiesDestroyed())
        {
            hasWon = true;
            SceneManager.LoadScene("WinningScene");
        }
    }

    private bool AllEnemiesDestroyed()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) return false;
        }
        return true;
    }
}
