using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    int speed = 8;
    int ignoreInput;
    float iciness;
    Rigidbody rb;
    Vector3 visualRotation;
    [Header("Ground")]
    public bool isGrounded = true;
    public LayerMask groundLayer;
    [Header("Camera")]
    public float rotationLimit = 80f;
    public static float sensitivity;
    public Transform visuals;
    [Header("Raycast")]
    public float raycastLength = 0.501f;
    public float playerWidth = 0.48f;
    public float playerDepth = 0.48f;
    [Header("Other")]
    public float acceleration = 60f;
    public ShootScript shootScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (ignoreInput < 5)
        {
            ignoreInput++;
            return;
        }

        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
        transform.Rotate(rotation);
        visualRotation.x = visualRotation.x + Input.GetAxis("Mouse Y") * sensitivity * -0.75f * Time.deltaTime;
        visualRotation.x = Mathf.Clamp(visualRotation.x, -rotationLimit, rotationLimit);
        visuals.localEulerAngles = visualRotation;

        if (Input.GetButton("Fire1") && !PauseMenu.GameIsPaused) 
        {
            shootScript.Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = Vector3.zero;

        isGrounded = false;
        bool slippery = false;
        for (int x = -1; x < 2; x++) 
        {
            for (int z = -1; z < 2; z++) 
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.TransformPoint(x * playerWidth, 0, z * playerDepth), Vector3.down, out hit, raycastLength, groundLayer.value, QueryTriggerInteraction.UseGlobal)) 
                {
                    isGrounded = true;
                    GenericMovement platform = GenericMovement.GetComponentFromGameObject(hit.collider.gameObject);
                    if (platform != null) 
                    {
                        targetVelocity = platform.velocity;
                    }
                    if (hit.collider.gameObject.CompareTag("Ice")) 
                    {
                        slippery = true;
                    }
                    if (hit.collider.gameObject.CompareTag("Bouncy"))
                    {
                        FindObjectOfType<AudioManager>().Play("Bouncy Sound");
                        jump(12);
                    }
                }
            }
        }

        if (slippery) 
        {
            iciness = 1;
        } else if (isGrounded) 
        {
            iciness = 0;
        } else 
        {
            iciness = Mathf.Clamp01(iciness - Time.fixedDeltaTime / 2);
        }

        targetVelocity = targetVelocity + transform.forward * speed * (iciness + 1) * Input.GetAxisRaw("Vertical");
        targetVelocity = targetVelocity + transform.right * speed * (iciness + 1) * Input.GetAxisRaw("Horizontal");
        Vector3 velocity = rb.velocity;
        targetVelocity.y = velocity.y;
        velocity = Vector3.MoveTowards(velocity, targetVelocity, Mathf.Lerp(1, 0.5f, iciness) * acceleration * Time.fixedDeltaTime);

        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        if (Input.GetButton("Jump") && isGrounded)
        {
            jump();
        }
    }

    public void jump(float jumpForce = 8)
    {
        Vector3 velocity = rb.velocity;
        velocity.y = Mathf.Max(jumpForce, velocity.y);
        rb.velocity = velocity;
    }

    public void resetRotation() 
    {
        visuals.localRotation = Quaternion.identity;
        visualRotation = Vector3.zero;
    }
}
