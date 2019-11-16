using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    public enum Mode { Default, Dense, Jump };

    // Public variables
    public Vector3 velocity;
    public Mode mode = Mode.Default;
    public float MAX_SPEED;
    public float drag;
    public float denseDrag;

    public static CustomController controllerInstance;

    // Private variables
    private Vector3 acceleration;
    private bool hasJumped;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);

        if (controllerInstance == null)
        {
            controllerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(mode)
        {
            case Mode.Default:
                DefaultMovement();
                break;

            case Mode.Dense:
                DenseMovement();
                break;

            case Mode.Jump:
                JumpMovement();
                break;
        }

        gameObject.GetComponent<Rigidbody>().AddForce(acceleration);

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(gameObject.GetComponent<Rigidbody>().velocity, MAX_SPEED);
        acceleration = Vector3.zero;
    }

    void AddForce(Vector3 force)
    {
        acceleration += force;
    }

    void DefaultMovement()
    {
        gameObject.GetComponent<Rigidbody>().drag = drag;
        AddForce(new Vector3(Input.GetAxis("Horizontal") * MAX_SPEED / gameObject.GetComponent<Rigidbody>().mass, 0));
    }

    void DenseMovement()
    {
        gameObject.GetComponent<Rigidbody>().drag = denseDrag;
    }

    void JumpMovement()
    {

    }
}
