using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public void DestroyParticle()
    {
        Destroy(gameObject);
    }
}