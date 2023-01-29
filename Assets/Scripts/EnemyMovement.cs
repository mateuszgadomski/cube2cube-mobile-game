using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Rigidbody rb;

    private Vector3 _direction;

    private void Start()
    {
        SetStartDirection(transform.up);
    }

    private void FixedUpdate()
    {
        Move(_direction, enemy.Settings.enemySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeDirection(collision);
    }

    private void Move(Vector3 direction, float speed) => rb.velocity = Vector3.ClampMagnitude(direction, speed);

    private void ChangeDirection(Collision collision)
    {
        if (collision.gameObject)
        {
            if (_direction != transform.up)
            {
                _direction = transform.up;
                enemy.Attack();
            }
            else
            {
                _direction = -transform.up;
            }
        }
    }

    private void SetStartDirection(Vector3 direction) => _direction = direction;
}