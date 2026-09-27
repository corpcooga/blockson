using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Shooting,
        LostSight
    }

    Vector3 startPosition;
    Vector3 startRotation;
    Transform player;
    Rigidbody rb;
    State currentState; 
    [Header("Raycast")]
    public Vector3 halfExtents = new Vector3(.5f, .5f, .5f);
    public float raycastLength = 0.501f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayers;
    [Header("Weapon")]
    public ShootScript shootScript;
    public Transform weaponVisuals;
    public Transform bulletSpawnPoint;
    [Header("Idle")]
    public float detectionRange = 20f;
    [Header("Shooting")]
    public float chaseRange = 28f;
    // Lost Sight
    Vector3 lastKnownLocation;
    public const float stopRange = 2f;
    [Header("Other")]
    public float speed = 2f;

    public static List<ShootingEnemy> shootingEnemies = new List<ShootingEnemy>();

    void Start()
    {
        shootingEnemies.Add(this);
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    void OnDestroy()
    {
        shootingEnemies.Remove(this);
    }

    public static void resetAllShootingEnemies()
    {
        foreach (ShootingEnemy enemy in shootingEnemies)
        {
            enemy.resetShootingEnemy();
        }
    }

    void FixedUpdate()
    {
        switch(currentState) 
        {
            case State.Idle:
                if (HasLineOfSight(detectionRange))
                {
                    currentState = State.Shooting;
                }
                break;
            case State.Shooting:
                if (HasLineOfSight(chaseRange))
                {
                    RotateAndShoot();
                    Move(player.position);
                    lastKnownLocation = player.position;
                } else 
                {
                    currentState = State.LostSight;
                    Move(lastKnownLocation);
                }
                break;
            case State.LostSight:
                if (!Move(lastKnownLocation) || (transform.position - lastKnownLocation).magnitude < stopRange)
                {
                    currentState = State.Idle;
                } else if (HasLineOfSight(detectionRange))
                {
                    currentState = State.Shooting;
                }
                break;
        }
    }

    void RotateAndShoot()
    {
        Vector3 lookPoint = player.position;
        // keeping the enemy body in the same x rotation
        lookPoint.y = transform.position.y;
        transform.LookAt(lookPoint);
        lookPoint = player.position;
        // convert to local
        lookPoint = weaponVisuals.InverseTransformPoint(lookPoint);
        // set x to 0 so it won't look left or right
        lookPoint.x = 0;
        // convert back to wold
        lookPoint = weaponVisuals.TransformPoint(lookPoint);
        weaponVisuals.LookAt(lookPoint);
        bulletSpawnPoint.LookAt(player.position);
        shootScript.Shoot();
    }

    bool Move(Vector3 targetPosition)
    {
        targetPosition.y = transform.position.y;
        Vector3 potential = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        bool canMove = EnemyMovement.checkPosition(transform, potential, halfExtents, raycastLength, groundLayer, obstacleLayers);
        if (canMove)
        {
            rb.MovePosition(potential);
        }
        return canMove;
    }

    bool HasLineOfSight(float range)
    {
        Vector3 raycastDirection = player.position - transform.position;
        float distanceToPlayer = raycastDirection.magnitude;
        raycastDirection /= distanceToPlayer;
        if (distanceToPlayer > range)
        {
            return false;
        }
        return !Physics.Raycast(transform.position, raycastDirection, distanceToPlayer, groundLayer.value);
    }

    public void resetShootingEnemy()
    {
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        currentState = State.Idle;
    }
}
