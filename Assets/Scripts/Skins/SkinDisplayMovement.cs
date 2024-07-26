using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkinDisplayMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody2D _rb;

    private void Awake() =>
        _rb = GetComponent<Rigidbody2D>();

    private void Update() =>
        Move();

    private void Move() =>
        _rb.velocity = new Vector2( speed, _rb.velocity.y);
}
