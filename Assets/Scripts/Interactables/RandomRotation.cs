using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{

    void Start()
    {
        transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Space.Self);
    }

    void Update()
    {
        Vector3 rotateSpeed = new Vector3(40, 30, 25);
        transform.Rotate(rotateSpeed * Time.deltaTime, Space.Self);
    }
}
