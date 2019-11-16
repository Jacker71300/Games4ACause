using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (character.GetComponent<CustomController>().isTransparent)
            gameObject.GetComponent<Collider>().isTrigger = true;
        else
            gameObject.GetComponent<Collider>().isTrigger = false;
    }
}
