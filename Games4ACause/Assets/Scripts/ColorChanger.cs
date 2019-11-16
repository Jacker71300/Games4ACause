using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public CustomController.Mode mode;

    private Collider characterCollider;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GameObject.Find("Character").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Collision();
    }

    public void Collision()
    {
        if (characterCollider.bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
        {
            CustomController.controllerInstance.mode = mode;
        }

    }
}
