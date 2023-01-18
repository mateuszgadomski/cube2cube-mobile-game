using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Vector3 direction;

    private void Start()
    {
        direction = Vector3.up;
    }

    private void Update()
    {
        Move(direction, enemy.settings.enemySpeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        ChangeDirection(collider);
    }

    private void Move(Vector3 direction, float cubeSpeed) => transform.Translate(direction * cubeSpeed * Time.deltaTime, Space.World);

    private void ChangeDirection(Collider collider)
    {
        if (collider.gameObject)
        {
            if (direction != Vector3.up)
            {
                direction = Vector3.up;
                enemy.Attack();
            }
            else
            {
                direction = Vector3.down;
            }
        }
    }
}