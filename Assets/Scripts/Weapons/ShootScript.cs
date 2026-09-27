using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    bool canShoot = true;
    float raycastDistance;
    public Collider parentCollider;
    public float timeBetweenShots = 0.5f;
    public GameObject bullet;
    public new Camera camera;
    public Transform bulletSpawnPoint;
    public LayerMask raycastLayers;

    public Animator animator;

    void Start()
    {
        raycastDistance = bullet.GetComponent<BulletScript>().speed * BulletScript.aliveTime;
    }

    public bool Shoot()
    {
        if (!canShoot) 
        {
            return false;
        }
        FindObjectOfType<AudioManager>().Play("Shoot Sound");
        canShoot = false;
        Vector3 raycastDirection;
        if (camera)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, raycastDistance, raycastLayers.value, QueryTriggerInteraction.UseGlobal))
            {
                raycastDirection = hit.point - bulletSpawnPoint.position;
            } else
            {
                raycastDirection = camera.transform.position + camera.transform.forward * raycastDistance - bulletSpawnPoint.position;
            }
        } else
        {
            raycastDirection = bulletSpawnPoint.forward;
        }
        GameObject bulletClone = (GameObject)Instantiate(bullet, bulletSpawnPoint.position, Quaternion.LookRotation(raycastDirection));
        animator.SetBool("Shooting", true);
        bulletClone.GetComponent<BulletScript>().parentCollider = parentCollider;
        StartCoroutine(ShootCooldown());
        return true;
    }

    void OnEnable()
    {
        canShoot = true;
    }

    IEnumerator ShootCooldown() 
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
        animator.SetBool("Shooting", false);
    }
}
