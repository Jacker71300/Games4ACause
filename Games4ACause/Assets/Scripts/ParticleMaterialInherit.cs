using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialInherit : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<ParticleSystemRenderer>().material = transform.parent.gameObject.GetComponent<Renderer>().material;
    }
}
