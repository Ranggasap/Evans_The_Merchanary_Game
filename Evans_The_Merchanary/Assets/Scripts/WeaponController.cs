using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip fireSFX;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.y > 0.1f)
        {
            weaponAnimator.Play("SenjataAtas");
        }
        else if (moveInput.y < -0.1f)
        {
            weaponAnimator.Play("SenjataBawah");
        }
        else
        {
            weaponAnimator.Play("SenjataKanan");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            SFXManager.instance.PlaySFXClip(fireSFX, transform.position);
            Vector2 direction = player.localScale.x < 0 ? -firePoint.right : firePoint.right;
            bulletController.SetDirection(direction);
        }
    }
}
