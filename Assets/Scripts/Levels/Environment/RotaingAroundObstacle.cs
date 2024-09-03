using UnityEngine;

public class RotaingAroundObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private bool _canMove;

    private void Start()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
            Rotate();
    }

    private void Rotate() =>
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
        
}
