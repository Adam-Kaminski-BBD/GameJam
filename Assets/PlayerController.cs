using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.0f;
    public ContactFilter2D movementFilter;
    public Animator animator;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameOverScreen GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        StartCoroutine(ChangeAnimationStateAfterDelay());
    }

    void Update() 
    {
        if(currentHealth <= 0) {
            GameOverScreen.Setup();
        }
    }

    private IEnumerator ChangeAnimationStateAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);

        // Change to the new animation state.
        animator.SetTrigger("idle");
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(5);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            
            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool("isMoving", success);
        }else{
            animator.SetBool("isMoving", false);
        }

        //set the direction of sprite to movement direction
        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        }else if(movementInput.x > 0){
            spriteRenderer.flipX = false;
        }

           // Clamp the player's position
           ClampPosition();
        
    }

    private bool TryMove(Vector2 direction)
    {
        if(direction != Vector2.zero){
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }else{
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    private void ClampPosition()
    {
        Vector3 newPosition = transform.position;

        // Get the screen boundaries in world coordinates
        Vector3 minBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        // Clamp the player's position within the screen boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }
}