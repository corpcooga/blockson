using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovement : MonoBehaviour
{
    public enum MovementDirection 
    {
        Forward,
        Back,
        Right,
        Left,
        Up,
        Down
    }
    public enum SmoothingType 
    {
        None,
        Sine
    }
    public enum UpdateMode
    {
        Update,
        UnscaledUpdate,
        FixedUpdate
    }

    float currentTime;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 previousPosition;
    MovementDirection startDirection;
    [Header("Enums")]
    public MovementDirection currentDirection;
    public SmoothingType smoothing;
    public UpdateMode updateMode;
    [Header("Movement")]
    public float duration;
    public float distance;

    public static Dictionary<GameObject, GenericMovement> allMovements = new Dictionary<GameObject, GenericMovement>();
    public Vector3 velocity{get;private set;}
    float previousTime;

    void Start()
    {
        startPosition = transform.position;
        allMovements[gameObject] = this;
        previousPosition = startPosition;
        rb = GetComponent<Rigidbody>();
    }

    void UpdatePosition(bool useRigidbody = false)
    {
        Vector3 endPosition;
        switch(currentDirection) 
        {
            case MovementDirection.Forward:
                endPosition = startPosition + distance * Vector3.forward;
                break;
            case MovementDirection.Back:
                endPosition = startPosition + distance * Vector3.back;
                break;
            case MovementDirection.Right:
                endPosition = startPosition + distance * Vector3.right;
                break;
            case MovementDirection.Left:
                endPosition = startPosition + distance * Vector3.left;
                break;
            case MovementDirection.Up:
                endPosition = startPosition + distance * Vector3.up;
                break;
            default:
                endPosition = startPosition + distance * Vector3.down;
                break;
        }
        float timing;
        switch(smoothing) 
        {
            default:
                timing = Mathf.PingPong(currentTime, duration) / duration;
                break;
            case SmoothingType.Sine:
                timing = (Mathf.Sin(currentTime * Mathf.PI / duration) + 1) / 2;
                break;
        }

        Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, timing);
        velocity = (newPosition - previousPosition) / (currentTime - previousTime);
        previousPosition = newPosition;
        previousTime = currentTime;
        
        if (useRigidbody) 
        {
            rb.MovePosition(newPosition);
        } else 
        {
            transform.position = newPosition;
        }
    }

    void Update() 
    {
        if (updateMode == UpdateMode.Update) 
        {
            currentTime += Time.deltaTime;
            UpdatePosition();
        }
        if (updateMode == UpdateMode.UnscaledUpdate) 
        {
            currentTime += Time.unscaledDeltaTime;
            UpdatePosition();
        }
    }

    void FixedUpdate()
    {
        if (updateMode == UpdateMode.FixedUpdate) 
        {
            currentTime += Time.fixedDeltaTime;
            UpdatePosition(rb != null);
        }
    }

    void OnDestroy() 
    {
        allMovements.Remove(gameObject);
    }

    public static GenericMovement GetComponentFromGameObject(GameObject gameObject) 
    {
        if (allMovements.ContainsKey(gameObject)) 
        {
            return allMovements[gameObject];
        }
        return null;
    }

    public static void resetAllGeneric() 
    {
        foreach (KeyValuePair<GameObject, GenericMovement> pair in allMovements)
        {
            pair.Value.resetSingleGeneric();
        }
    }

    public void resetSingleGeneric()
    {
        currentTime = 0;
        UpdatePosition();
    }
}
