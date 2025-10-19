using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    private CharacterController cc;
    private Vector3 velocity;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal"); // A/D, Left/Right
        float v = Input.GetAxis("Vertical");   // W/S, Up/Down
        Vector3 move = new Vector3(h, 0, v);
        if (move.magnitude > 0.1f)
        {
            cc.Move(transform.TransformDirection(move.normalized) * moveSpeed * Time.deltaTime);
        }

        // Gravity
        if (!cc.isGrounded) velocity.y += gravity * Time.deltaTime;
        else velocity.y = -1f;
        cc.Move(velocity * Time.deltaTime);
    }

    public void ApplyDamage(int amount)
    {
        // Placeholder - link to Health system
        Debug.Log(gameObject.name + " took damage: " + amount);
    }
}
