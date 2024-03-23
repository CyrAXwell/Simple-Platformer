using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float idleTime;
    [SerializeField] bool isLoop;

    private Vector3 _targetPosition;
    private int _index = 1;
    private int _direction = 1;
    private bool canMove = true;

    private void Awake()
    {
        _targetPosition = waypoints[_index].position;
    }

    private void Update()
    {
        CheckTargetPosition();
        if (canMove)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    private void CheckTargetPosition()
    {
        if (transform.position == _targetPosition)
        {
            StartCoroutine(Stay(idleTime));
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        if (isLoop)
        {
            if (_index == waypoints.Count - 1)
                _index = 0; 
            else
                _index += _direction;
        }
        else
        {
            if (_index == waypoints.Count - 1 || _index == 0)
            {
                _direction *= -1;
                _index += _direction; 
            }  
            else
            {
                _index += _direction;
            } 
        }
        
        _targetPosition = waypoints[_index].position;
    }

    private IEnumerator Stay(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

}

