using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject bulletPrefab;
    public GameObject firePrefab;
    public float bulletSpeed = 0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001f;

    public MenuController menuController;
    public GameObject menuCanvas;

    public Animator bulletAnimator;

    public WeaponController weaponController;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        Vector2 playerPosition = transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseClickPosScreen = Input.mousePosition;
            Vector2 mouseClickPosWorld = Camera.main.ScreenToWorldPoint(mouseClickPosScreen);

            GetSetDistance(playerPosition, mouseClickPosWorld);
            HandleAttack(mouseClickPosWorld);
        }
    }

    void HandleAttack(Vector2 targetPosition)
    {
        string bang = weaponController.getWeapon();

        switch (bang)
        {
            case "fire":
                Debug.Log("fire attack");
                SprayFire(targetPosition);
                break;

            case "pistol":
                FireBullet(targetPosition);
                break;

            case "slime":
                Debug.Log("Slime attack");
                break;

            case "wall":
                Debug.Log("Wall attack");
                break;

            default:
                Debug.Log("NOT FIRE OR PISTOL");
                return;
        }
    }

    void GetSetDistance(Vector2 playerPosition, Vector2 mouseClickPosWorld)
    {
        float dist = Vector2.Distance(playerPosition, mouseClickPosWorld);
        // You can use 'dist' here if needed.
    }

    void FireBullet(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        bullet.transform.rotation = RotateProjectile(direction);

        bullet.SetActive(true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 1f;
        Destroy(bullet, 5f);
    }

    void SprayFire(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(firePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        bullet.transform.rotation = RotateProjectile(direction);

        bullet.SetActive(true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 0.5f;
        Debug.Log(rb.velocity.magnitude);
        Destroy(bullet, 2f);
    }

    Quaternion RotateProjectile(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
}
