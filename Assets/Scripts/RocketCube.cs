using UnityEngine;

public class RocketCube : Enemy
{
    public override void Attack()
    {
        base.Attack();

        Debug.Log("Rocket");
    }
}