﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    public enum Mode { Default, Dense, Jump, InvertGravity, Transparent };
    public bool gravityInverted = false;
    public bool isTransparent = false;

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
    private Mode previousMode;
    private float gravityCooldown;
    private float transparentCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0);
        acceleration = new Vector3(0, 0);
        lastVelocity = new Vector3(0, 0);
        gravityCooldown = 0;
        transparentCooldown = 0;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        previousMode = mode;

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

            case Mode.InvertGravity:
                InvertGravityMovement();
                break;

            case Mode.Transparent:
                TransparentMovement();
                break;

        }

        if (gravityCooldown > 0)
            gravityCooldown -= Time.deltaTime;
        if (transparentCooldown > 0)
            transparentCooldown -= Time.deltaTime;

        if(Mathf.Abs(lastVelocity.x) > 1 && Mathf.Abs(rigidbody.velocity.x) < .3 )
        {
            rigidbody.velocity = new Vector3(-lastVelocity.x, rigidbody.velocity.y);
        }

        rigidbody.AddForce(acceleration);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MAX_SPEED);
        acceleration = Vector3.zero;
        lastVelocity = rigidbody.velocity;
        previousMode = mode;
    }

    void AddForce(Vector3 force)
    {
        acceleration += force;
    }

    // Allow normal movement
    void DefaultMovement()
    {
        rigidbody.drag = drag;
        AddForce(new Vector3(Input.GetAxis("Horizontal") * MAX_SPEED / rigidbody.mass, 0));
    }

    // Create the extra friction from the dense property
    void DenseMovement()
    {
        rigidbody.drag = denseDrag;
    }

    // Jump and reset the jump when hitting the ground
    void JumpMovement()
    {
        if(Mathf.Abs(lastVelocity.x) - Mathf.Abs(rigidbody.velocity.x) >= 0 && (Mathf.Abs(lastVelocity.y) - Mathf.Abs(rigidbody.velocity.y)) < 0)
        {
            hasJumped = true;
        }
        // Jump if inverted
        if (gravityInverted)
        {
            if (Input.GetAxis("Jump") != 0 && !hasJumped)
            {
                AddForce(new Vector3(0, -jumpForce));
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f);
                hasJumped = true;
            }
            else if ((hasJumped && rigidbody.velocity.y - lastVelocity.y < -1 && lastVelocity.y > 0 && rigidbody.velocity.y <= 0) || (rigidbody.velocity.y == 0 && lastVelocity.y == 0))
            {
                UnityEngine.Physics.gravity /= gravityMultiplier;
                hasJumped = false;
            }
            else if (hasJumped && rigidbody.velocity.y >= 0 && lastVelocity.y < 0)
            {
                UnityEngine.Physics.gravity *= gravityMultiplier;
            }
        }
        else // Jump normally
        {
            if (Input.GetAxis("Jump") != 0 && !hasJumped)
            {
                AddForce(new Vector3(0, jumpForce));
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f);
                hasJumped = true;
            }
            else if (hasJumped && rigidbody.velocity.y - lastVelocity.y > 1 && lastVelocity.y < 0 && rigidbody.velocity.y <= 0 || (rigidbody.velocity.y == 0 && lastVelocity.y == 0))
            {
                UnityEngine.Physics.gravity /= gravityMultiplier;
                hasJumped = false;
            }
            else if (hasJumped && rigidbody.velocity.y <= 0 && lastVelocity.y > 0)
            {
                UnityEngine.Physics.gravity *= gravityMultiplier;
            }
        }
    }

    // Inverts gravity
    void InvertGravityMovement()
    {
        if (gravityCooldown <= 0)
        {
            UnityEngine.Physics.gravity *= -1;
            gravityInverted = !gravityInverted;
            gravityCooldown = 2f;
        }
        mode = previousMode;
    }

    // Handles Transparency
    void TransparentMovement()
    {
        if (transparentCooldown <= 0)
        {
            isTransparent = !isTransparent;
        }
        mode = previousMode;
    }
}