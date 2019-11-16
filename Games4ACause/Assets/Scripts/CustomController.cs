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
    public float jumpForce;
    public float gravityMultiplier = 3f;

    public static CustomController controllerInstance;

    // Private variables
    private Vector3 acceleration;
    private bool hasJumped;
    private Vector3 lastVelocity;
    private Rigidbody rigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);
        lastVelocity = new Vector3(0, 0);
        rigidbody = gameObject.GetComponent<Rigidbody>();

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

        if(Mathf.Abs(lastVelocity.x) > 1 && Mathf.Abs(rigidbody.velocity.x) < .1 )
        {
            rigidbody.velocity = new Vector3(-lastVelocity.x, rigidbody.velocity.y);
        }

        rigidbody.AddForce(acceleration);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MAX_SPEED);
        acceleration = Vector3.zero;
        lastVelocity = rigidbody.velocity;
    }

    void AddForce(Vector3 force)
    {
        acceleration += force;
    }

    void DefaultMovement()
    {
        rigidbody.drag = drag;
        AddForce(new Vector3(Input.GetAxis("Horizontal") * MAX_SPEED / rigidbody.mass, 0));
    }

    void DenseMovement()
    {
        rigidbody.drag = denseDrag;
    }

    void JumpMovement()
    {

        if (Input.GetAxis("Jump") != 0 && !hasJumped)
        {
            AddForce(new Vector3(0, jumpForce));
            hasJumped = true;
        }
        else if (hasJumped && rigidbody.velocity.y == 0 && lastVelocity.y < 0) 
        {
            UnityEngine.Physics.gravity /= gravityMultiplier;
            hasJumped = false;
        }
        else if(hasJumped && rigidbody.velocity.y <= 0 && lastVelocity.y > 0)
        {
            UnityEngine.Physics.gravity *= gravityMultiplier;
        }
    }
}
