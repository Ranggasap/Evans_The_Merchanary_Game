using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int playerHealth = 3;
    [SerializeField] private UnityEvent<int> changeHealth;
    [SerializeField] private float immuneDuration = 1f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private AudioClip gotHitSFX;

    private bool isImmune = false;
    private float immuneTimer = 0f;

    private void Start()
    {
        changeHealth.Invoke(playerHealth);
    }

    private void Update()
    {
        if (isImmune)
        {
            immuneTimer -= Time.deltaTime;
            if (immuneTimer <= 0f)
            {
                isImmune = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayerMask) != 0)
        {
            SFXManager.instance.PlaySFXClip(gotHitSFX, transform.position);
            if (!isImmune)
            {
                TakeDamage(1);
                isImmune = true;
                immuneTimer = immuneDuration;
            }
        }
    }

    public void AddHealth(int amount)
    {
        playerHealth += amount;
        changeHealth.Invoke(playerHealth);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        changeHealth.Invoke(playerHealth);

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
