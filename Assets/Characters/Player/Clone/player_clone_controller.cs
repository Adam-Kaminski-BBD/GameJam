using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_clone_controller : MonoBehaviour
{
    public bool isMoving;
    public Animator animator;

    //bullet
    public GameObject bulletPrefab;
    public GameObject firePrefab;
    public float bulletSpeed = 0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001f;
    public GameObject slimePrefab;
    public GameObject wallPrefab;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       
    }

    public void HandleWeapon(Vector2 weaponPosition, string currentWeapon, Quaternion rotation)
    {
        if(weaponPosition != new Vector2(100f, 100f))
        {
            Vector2 value = weaponPosition;
            switch (currentWeapon)
            {
                case "fire":
                    SprayFire(weaponPosition);
                    break;

                case "pistol":
                    FireBullet(weaponPosition);
                    break;

                case "slime":
                    GooiSlime(weaponPosition);
                    break;

                case "wall":
                    //being called twice in succession
                    PlaceWall(weaponPosition, rotation);
                    break;

                default:
                    Debug.Log("Not a valid choice");
                    return;
            }
        }
    }

    //used to shoot bullet based on the target position saved.
    void FireBullet(Vector2 targetPosition)
    {
        // Vector2(100,100) is the default for invalid or no shot
        if (targetPosition != new Vector2(100, 100))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            bullet.transform.rotation = RotateProjectile(direction);

            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * 1f;
            Destroy(bullet, 5f);
        }
    }

    Quaternion RotateProjectile(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    void GooiSlime(Vector2 targetPosition)
    {
        GameObject slime = Instantiate(slimePrefab, targetPosition, Quaternion.identity);

        // Calculate the direction from the player to the click position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Calculate the rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slime.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

        // Apply an initial force to the slime to make it move
        Rigidbody2D rb = slime.GetComponent<Rigidbody2D>();

        slime.SetActive(true);

        Destroy(slime, 5f);

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

    void PlaceWall(Vector2 position, Quaternion rotation)
    {
        Instantiate(wallPrefab, position, rotation);
    }

    public void UpdateAnim(AnimatorStateInfo movement, bool flipX, GameObject revolver)
    {
        spriteRenderer.flipX = flipX;
        GameObject child = transform.Find("Revolver").gameObject;
        child.GetComponent<SpriteRenderer>().flipX = flipX;
        if (movement.IsName("player_spawn"))
        {
            //do nothing, its entry
        }
        else if (movement.IsName("player_idle"))
        {
            animator.SetTrigger("idle");
            animator.SetBool("isMoving", false);
        }
        else if (movement.IsName("player_walk"))
        {
            animator.SetBool("isMoving", true);
        }
    }
}
