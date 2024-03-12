using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravityScale = 10f;
    [SerializeField] private float jumpCooldown = 0.2f;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D _rb;
    private float _jumpCooldownCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        Move();
        _jumpCooldownCounter -= Time.deltaTime;
    }

    private void Move() =>
        _rb.velocity = new Vector2( speed, _rb.velocity.y);

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && _jumpCooldownCounter <= 0f)
            _rb.gravityScale *= -1f;
    }

    private bool IsGrounded()
    {
        RaycastHit2D rayCastHitUpward = Physics2D.Raycast(transform.position, Vector2.up, transform.localScale.y / 2f + 0.2f, whatIsGround);
        RaycastHit2D rayCastHitDownward = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y / 2f + 0.2f, whatIsGround);

        return rayCastHitUpward.collider != null || rayCastHitDownward.collider != null;
    }
}
