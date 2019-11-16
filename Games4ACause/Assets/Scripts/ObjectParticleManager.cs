using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParticleManager : MonoBehaviour
{
    [System.Serializable]
    public struct NamedParticleSystem
    {
        public string name;
        public ParticleSystem particleSystem;
    }

    public NamedParticleSystem[] particleSystems;

    private Dictionary<string, ParticleSystem> particleSystemDict;
    
    // Start is called before the first frame update
    void Start()
    {
        particleSystemDict = new Dictionary<string, ParticleSystem>();
        foreach(NamedParticleSystem ps in particleSystems)
        {
            var main = ps.particleSystem.main;
            particleSystemDict[ps.name] = ps.particleSystem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveMessage(string particleSystem, string action)
    {
        switch(action)
        {
            case "play":
                particleSystemDict[particleSystem].Play();
                break;
        }
    }
}
