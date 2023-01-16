using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCube : Enemy
{


public override void EnemyAttack()
    {
        base.EnemyAttack();

        Debug.Log("Rocket");
    }
}
