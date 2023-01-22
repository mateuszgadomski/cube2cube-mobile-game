using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private Vector3 _direction;

    private void Start()
    {
        SetStartDirection(Vector3.up);
    }

    private void Update()
    {
        Move(_direction, enemy.settings.enemySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeDirection(collision);
    }

    private void Move(Vector3 direction, float cubeSpeed) => transform.Translate(direction * cubeSpeed * Time.deltaTime);

    private void ChangeDirection(Collision collision)
    {
        if (collision.gameObject)
        {
            if (_direction != Vector3.up)
            {
                _direction = Vector3.up;
                enemy.Attack();
            }
            else
            {
                _direction = Vector3.down;
            }
        }
    }

    private void SetStartDirection(Vector3 direction) => _direction = direction;
}