using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    public enum Mode { Default, Dense, Jump };

    // Public variables
    public Vector3 velocity;
    Mode mode = Mode.Default;
    public float MAX_SPEED;
    public float mass;


    // Private variables
    private Vector3 acceleration;
    private bool hasJumped;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);
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

        velocity = acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, MAX_SPEED);
        gameObject.transform.position = velocity * Time.deltaTime;

        acceleration = Vector3.zero;
    }

    void AddForce(Vector3 force)
    {
        acceleration += force;
    }

    void DefaultMovement()
    {
        AddForce(new Vector3(Input.GetAxis("x") * MAX_SPEED / mass, 0));
    }

    void DenseMovement()
    {
        AddForce(new Vector3(-velocity.normalized.x * mass / MAX_SPEED, 0));
    }

    void JumpMovement()
    {

    }
}
