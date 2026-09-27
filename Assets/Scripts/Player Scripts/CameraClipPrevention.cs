using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClipPrevention : MonoBehaviour
{
    float raycastDistance;
    Vector3 startPosition;
    Vector3 raycastDirection;
    public const float pushForward = 0.5f;
    public LayerMask detectionLayers;

    void Start()
    {
        startPosition = transform.localPosition;
        raycastDistance = startPosition.magnitude + pushForward;
        raycastDirection = startPosition / raycastDistance;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.TransformDirection(raycastDirection), out hit, raycastDistance, detectionLayers.value, QueryTriggerInteraction.UseGlobal))
        {
            transform.position = hit.point - pushForward * transform.parent.TransformDirection(raycastDirection);
        } else 
        {
            transform.localPosition = startPosition;
        }
    }
}
