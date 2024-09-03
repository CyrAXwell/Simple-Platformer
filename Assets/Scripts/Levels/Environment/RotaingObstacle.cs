using UnityEngine;

public class RotaingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;

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
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);

}
