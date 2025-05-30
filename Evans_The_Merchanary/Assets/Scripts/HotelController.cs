using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HotelController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private TextSO enterHotel;
    [SerializeField] private TextSO killAllEnemy;
    [SerializeField] private TextSO pressKeyE;
    [SerializeField] private UnityEvent<TextSO> changeObjectiveText;
    private bool playerInTrigger = false;
    private bool allEnemiesDefeated = false;

    private void Start()
    {
        changeObjectiveText.Invoke(killAllEnemy);
    }

    private void Update()
    {
        if (!allEnemiesDefeated)
        {
            allEnemiesDefeated = AreAllEnemiesDestroyed();
        }

        if (allEnemiesDefeated && playerInTrigger)
        {
            Debug.Log("Player boleh masuk");
            changeObjectiveText.Invoke(pressKeyE);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("BossIntro");
            }
        }

        if (allEnemiesDefeated && !playerInTrigger)
        {
            changeObjectiveText.Invoke(enterHotel);
        }
    }

    private bool AreAllEnemiesDestroyed()
    {
        foreach (GameObject enemy in enemyPrefabs)
        {
            if (enemy != null)
                return false;
        }
        changeObjectiveText.Invoke(enterHotel);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            playerInTrigger = false;
        
        }
    }
}
