using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float gravityScale = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask UIlayer;

    private Rigidbody2D _rb;
    private bool _isBlocked;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = gravityScale;   
    }

    private void Update()
    {
        Move();
    }

    private void Move() =>
        _rb.velocity = new Vector2( speed, _rb.velocity.y);

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if(results.Where(ray => ray.gameObject.tag == "UI Blocked").Count() > 0)
                _isBlocked = true;
            else
                _isBlocked = false;

            if (IsGrounded() && !_isBlocked)
                _rb.gravityScale *= -1f;

        }
    }

    private bool IsGrounded()
    {
        float distance = transform.localScale.y / 2f + 0.5f;

        RaycastHit2D rayCastHitUpward = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsGround);
        RaycastHit2D rayCastHitDownward = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsGround);

        return rayCastHitUpward.collider != null || rayCastHitDownward.collider != null;
    }
}
