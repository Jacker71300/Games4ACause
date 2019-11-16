using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public CustomController.Mode mode;

    private Material color;

    private Collider characterCollider;

    public GameObject character;

    private Material characterMaterial;



    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GameObject.Find("Character").GetComponent<Collider>();

        switch (mode)
        {
            case CustomController.Mode.Default:
                color = Resources.Load("Materials/Black", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Dense:
                color = Resources.Load("Materials/Red", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Jump:
                color = Resources.Load("Materials/White", typeof(Material)) as Material;
                break;
        }

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
            character.GetComponent<Renderer>().material = color;
            
        }

    }
}
