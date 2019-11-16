using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public CustomController.Mode mode;

    private Material color;

    private Collider characterCollider;
    private Collider myCollider;

    public GameObject character;

    private Material characterMaterial;

    private Vector3 originalPosition;



    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        characterCollider = character.GetComponent<Collider>();
        myCollider = gameObject.GetComponent<Collider>();
        originalPosition = gameObject.transform.position;

        switch (mode)
        {
            case CustomController.Mode.Default:
                color = Resources.Load("Materials/Black", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Dense:
                color = Resources.Load("Materials/Red", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Jump:
                color = Resources.Load("Materials/Blue", typeof(Material)) as Material;
                break;
        }

        GetComponent<Renderer>().material = color;

    }

    // Update is called once per frame
    void Update()
    {
        Collision();
    }

    public void Collision()
    {
        if (characterCollider.bounds.Intersects(myCollider.bounds))
        {
            CustomController.controllerInstance.mode = mode;
            character.GetComponent<Renderer>().material = color;

            if (transform.position.y > originalPosition.y - .2f)
                transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);
        }
        else if(transform.position.y < originalPosition.y)
            transform.position = new Vector3(transform.position.x, transform.position.y + .02f, transform.position.z);
    }
}
