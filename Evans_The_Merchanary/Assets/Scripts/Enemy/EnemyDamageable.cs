using UnityEngine;

public class EnemyDamageable : MonoBehaviour
{
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private AudioClip dieSFX;
    [SerializeField] private int health = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & bulletLayer) != 0)
        {
            health--;

            if (health <= 0)
            {
                SFXManager.instance.PlaySFXClip(dieSFX, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
