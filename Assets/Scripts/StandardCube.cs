using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardCube : Cube
{
    public override void CubeAttackEffect()
    {
        base.CubeAttackEffect();
        Debug.Log("Standard");
    }
}
