using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCube : Cube
{


public override void CubeAttackEffect()
    {
        base.CubeAttackEffect();

        Debug.Log("Rocket");
    }
}
