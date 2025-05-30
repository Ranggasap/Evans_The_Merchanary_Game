using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform player;

    private void FixedUpdate()
    {
        DetectAndChasePlayer();
    }

    private void DetectAndChasePlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        Debug.Log($"Mendeteksi Sesuatu: {hits}");
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            Debug.Log("Mendeteksi Player");
            Vector2 direction = (player.position - transform.position).normalized;
            spriteRenderer.flipX = player.position.x < transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // This still visually helps in the Scene view for 2D
    }
}
