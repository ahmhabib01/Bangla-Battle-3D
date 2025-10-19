using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;
    
    [Header("Combat Settings")]
    public int health = 100;
    public int maxHealth = 100;
    public string characterType;
    
    private Rigidbody rb;
    private Camera playerCamera;
    private float xRotation = 0f;
    
    // Character-specific abilities
    private bool canUseSpecialAbility = true;
    private float specialAbilityCooldown = 10f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        
        InitializeCharacterAbilities();
    }
    
    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
        HandleCombat();
        HandleSpecialAbility();
    }
    
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    
    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    void HandleCombat()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            PerformAttack();
        }
        
        if (Input.GetKeyDown(KeyCode.R)) // Reload or special action
        {
            PerformCharacterAction();
        }
    }
    
    void HandleSpecialAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canUseSpecialAbility)
        {
            ActivateSpecialAbility();
            StartCoroutine(SpecialAbilityCooldown());
        }
    }
    
    void InitializeCharacterAbilities()
    {
        characterType = GameManager.Instance.selectedCharacter;
        
        switch (characterType)
        {
            case "major":
                // Drone strike ability
                break;
            case "polash":
                // Fast swimming ability
                moveSpeed *= 1.2f;
                break;
            case "rokeya":
                // Book blast attack
                break;
            case "bhombol":
                // Bus horn stun
                break;
        }
    }
    
    void PerformAttack()
    {
        // Raycast for attack
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(25);
                }
            }
        }
    }
    
    void ActivateSpecialAbility()
    {
        switch (characterType)
        {
            case "major":
                // Call drone strike
                break;
            case "rokeya":
                // Book blast area attack
                break;
            case "bhombol":
                // Stun nearby enemies
                break;
        }
    }
    
    System.Collections.IEnumerator SpecialAbilityCooldown()
    {
        canUseSpecialAbility = false;
        yield return new WaitForSeconds(specialAbilityCooldown);
        canUseSpecialAbility = true;
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Handle player death
        GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
    }
}
