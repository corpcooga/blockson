using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementDirection 
    {
        Right = 1,
        Left = -1,
        Forward = 2,
        Back = -2
    }
    Rigidbody rb;
    [Header("Direction")]
    public MovementDirection startDirection;
    MovementDirection currentDirection;
    [Header("Raycast")]
    public Vector3 halfExtents = new Vector3(.5f, .5f, .5f);
    public float raycastLength = 0.55f;
    [Header("LayerMasks")]
    public LayerMask groundLayer;
    public LayerMask obstacleLayers;
    [Header("Speed")]
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetDirection();
    }

    void FixedUpdate()
    {
        Vector3 movementDirection;
        switch(currentDirection) 
        {
            default:
                movementDirection = transform.forward;
                break;
            case MovementDirection.Back:
                movementDirection = -transform.forward;
                break;
            case MovementDirection.Right:
                movementDirection = transform.right;
                break;
            case MovementDirection.Left:
                movementDirection = -transform.right;
                break;
        }

        Vector3 potential = transform.position + movementDirection * speed * Time.deltaTime;
        if (checkPosition(transform, potential, movementDirection, speed, halfExtents, raycastLength, groundLayer, obstacleLayers))
        {
            rb.MovePosition(potential);
        } else 
        {
            currentDirection = (MovementDirection)((int)currentDirection * -1);
        }
    }

    public static bool checkPosition(Transform transform, Vector3 potential, Vector3 movementDirection, float speed, Vector3 halfExtents, float raycastLength, LayerMask groundLayer, LayerMask obstacleLayers)
    {
        float halfWidth = halfExtents.x;
        halfExtents.y -= 0.05f;
        return Physics.Raycast(potential + movementDirection * halfWidth, Vector3.down, raycastLength, groundLayer.value) && 
        !Physics.Raycast(potential + movementDirection * halfWidth, Vector3.down, raycastLength, obstacleLayers.value) &&
        !Physics.CheckBox(potential, halfExtents, transform.rotation, obstacleLayers.value) &&
        !Physics.CheckBox(potential, halfExtents, transform.rotation, groundLayer.value);
    }

    public static bool checkPosition(Transform transform, Vector3 potential, Vector3 halfExtents, float raycastLength, LayerMask groundLayer, LayerMask obstacleLayers)
    {
        Vector3 movementDirection = potential - transform.position;
        float movementDistance = movementDirection.magnitude;
        movementDirection /= movementDistance;
        float speed = movementDistance / Time.deltaTime;
        return checkPosition(transform, potential, movementDirection, speed, halfExtents, raycastLength, groundLayer, obstacleLayers);
    }

    public void ResetDirection() 
    {
        currentDirection = startDirection;
    }
}