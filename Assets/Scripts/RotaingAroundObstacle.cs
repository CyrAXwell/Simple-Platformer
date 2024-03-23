using UnityEngine;
using UnityEngine.UIElements;

public class RotaingAroundObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
    }
}
