using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Vector2 lastPositionShot;
    private LineRenderer lineRenderer;
    public GameObject bulletPrefab;
    public GameObject firePrefab;
    public GameObject slimePrefab;

    private MenuController menuController;
    public GameObject menuCanvas;
    private bool menuShown = true;

    public Animator bulletAnimator;

    public WeaponController weaponController;

    public GameObject gridController;
    public GameObject wallPreview;
    public string currentWeapon;

    private Player_Replay replay;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        replay = GetComponentInParent<Player_Replay>();
        menuController = menuCanvas.GetComponent<MenuController>();
    }

    void Update()
    {
        menuShown = menuController.isActive;
        Vector2 playerPosition = transform.position;
        //bad code ik, but we don't need to refactor for a gamejam
        currentWeapon = weaponController.getWeapon();
        string isWall = weaponController.getWeapon();
        if (isWall == "wall")
        {
            placeWall();
        }
        else
        {
            removeWall();
        }
        if (Input.GetMouseButtonDown(0) && !menuShown)
        {
            Vector2 mouseClickPosScreen = Input.mousePosition;
            Vector2 mouseClickPosWorld = Camera.main.ScreenToWorldPoint(mouseClickPosScreen);
            lastPositionShot = mouseClickPosWorld;
            GetSetDistance(playerPosition, mouseClickPosWorld);
            HandleAttack(mouseClickPosWorld);
            replay.triggerClick();
        }
        
    }

    void HandleAttack(Vector2 targetPosition)
    {
        string bang = weaponController.getWeapon();
        switch (bang)
        { 
            case "fire":
                removeWall();
                SprayFire(targetPosition);
                break;

            case "pistol":
                removeWall();
                FireBullet(targetPosition);
                break;

            case "slime":
                removeWall();
                GooiSlime(targetPosition);
                break;

            case "wall":
                placeWall();
                break;

            default:
                removeWall();
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

    void placeWall()
    {
        gridController.GetComponent<Grid_System>().setOn();
        wallPreview.GetComponent<PlacementAreaIndicator>().enable();
    }
    void removeWall()
    {
        gridController.GetComponent<Grid_System>().setOff();
        wallPreview.GetComponent<PlacementAreaIndicator>().disable();
    }

    void GooiSlime(Vector2 targetPosition)
    {
        GameObject slime = Instantiate(slimePrefab, targetPosition, Quaternion.identity);

        // Calculate the direction from the player to the click position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Calculate the rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slime.transform.rotation = Quaternion.Euler(0, 0, angle+90f);

        // Apply an initial force to the slime to make it move
        Rigidbody2D rb = slime.GetComponent<Rigidbody2D>();

        slime.SetActive(true);

        Destroy(slime, 5f);

    }
}   
