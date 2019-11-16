using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject character;
    public Collider characterCollider;
    public Collider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        characterCollider = character.GetComponent<Collider>();
        foreach(Collider c in gameObject.GetComponents<Collider>())
        {
            if(c.isTrigger)
            {
                myCollider = c;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(characterCollider.bounds.Intersects(myCollider.bounds) && character.GetComponent<CustomController>().mode == CustomController.Mode.Dense)
        {
            gameObject.GetComponent<ObjectParticleManager>().ReceiveMessage("break", "play");

            //save the children!!!!!
            //(make the particle system not immediately disappear)
            foreach (Transform child in transform) child.parent = null;
        
            Destroy(gameObject);
        }
    }
}
    