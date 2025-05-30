using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Rigidbody2D rb;

    public void SetDirection(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}


