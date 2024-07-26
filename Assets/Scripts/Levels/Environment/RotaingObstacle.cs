using UnityEngine;

public class RotaingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate() =>
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);

}
