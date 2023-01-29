using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _direction;

    private void Start()
    {
        SetStartDirection(transform.up);
    }

    private void FixedUpdate()
    {
        Move(_direction, _enemy.Settings.EnemySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeDirection(collision);
    }

    private void SetStartDirection(Vector3 direction) => _direction = direction;

    private void Move(Vector3 direction, float speed) => _rb.velocity = Vector3.ClampMagnitude(direction, speed);

    private void ChangeDirection(Collision collision)
    {
        if (collision.gameObject)
        {
            if (_direction != transform.up)
            {
                _direction = transform.up;
                _enemy.Attack();
            }
            else
            {
                _direction = -transform.up;
            }
        }
    }
}