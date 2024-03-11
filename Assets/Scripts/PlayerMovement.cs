using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravityScale = 10f;
    [SerializeField] private float jumpCooldown = 0.2f;

    private Rigidbody2D _rb;
    private float _jumpCooldownCounter;
    private bool grounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Move();
        _jumpCooldownCounter -= Time.deltaTime;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            _rb.gravityScale *= -1f;
        } 
    }

    private void Move()
    {
        _rb.velocity = new Vector2( speed, _rb.velocity.y);
    }
}
