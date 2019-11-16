using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public CustomController.Mode mode;

    private Material material;

    private Collider characterCollider;
    private Collider myCollider;

    public GameObject character;

    private Material characterMaterial;

    private Vector3 originalPosition;

    public float depressionSpeed;

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
                material = Resources.Load("Materials/Black", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Dense:
                material = Resources.Load("Materials/Red", typeof(Material)) as Material;
                break;
            case CustomController.Mode.Jump:
                material = Resources.Load("Materials/Blue", typeof(Material)) as Material;
                break;
            case CustomController.Mode.InvertGravity:
                material = Resources.Load("Materials/Green", typeof(Material)) as Material;
                break;
        }

        GetComponent<Renderer>().material = material;

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
            if (character.GetComponent<CustomController>().mode == CustomController.Mode.InvertGravity)
            {
                //don't change color on gravity
            } else
            {
                character.GetComponent<Renderer>().material = material;
                if (mode == CustomController.Mode.InvertGravity && character.GetComponent<CustomController>().gravityCooldown <= 0)
                {
                    CustomController.controllerInstance.mode = mode;
                }
                else if (mode == CustomController.Mode.Transparent && character.GetComponent<CustomController>().transparentCooldown <= 0)
                {
                    CustomController.controllerInstance.mode = mode;
                }
                else
                {
                    CustomController.controllerInstance.mode = mode;
                    
                    if(mode != CustomController.Mode.Transparent)
                        character.GetComponent<Renderer>().material = material;
                }

                Color color = character.GetComponent<Renderer>().material.color;

                if (character.GetComponent<CustomController>().isTransparent)
                {                 
                    character.GetComponent<Renderer>().material.SetColor("Color", new Color(color.r, color.g, color.b, .5f));
                }
                else
                {
                    color = new Color(color.r, color.g, color.b, 1f);
                }
            }

            if (pathPercent(transform.position, originalPosition, originalPosition - transform.up * 0.2f) < 0.98)
                transform.position += -transform.up * depressionSpeed * Time.deltaTime;
        }
        else if(pathPercent(transform.position, originalPosition, originalPosition - transform.up * 0.2f) > 0.02)
            transform.position += transform.up * depressionSpeed * Time.deltaTime;
    }

    private float pathPercent(Vector3 currentPoint, Vector3 p1, Vector3 p2)
    {
        if (Vector3.Dot((currentPoint - p1).normalized, (p2 - p1).normalized) >= 0.99)
        {
            return (currentPoint - p1).magnitude / (p2 - p1).magnitude;
        } else
        {
            return -(currentPoint - p1).magnitude / (p2 - p1).magnitude;
        }
        
    }
}
