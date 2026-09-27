using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int speed = 50;
    public const float aliveTime = 1f;
    public static List<BulletScript> allBullets = new List<BulletScript>();
    public string[] tagsToHit = new string[0];
    public string[] tagsToEffect = new string[0];
    public Rigidbody rb;
    [HideInInspector]
    public Collider parentCollider;

    public ParticleSystem hitEffect;
    public AudioSource hitSound;

    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, aliveTime);
        allBullets.Add(this);
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        parentCollider.enabled = false;
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.fixedDeltaTime, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal))
        {
            GameObject otherGameObject = hit.transform.gameObject;
            for (int i = 0; i < tagsToEffect.Length; i++)
            {
                if (otherGameObject.CompareTag(tagsToEffect[i]))
                {
                    hitEffect.transform.parent = null;
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.forward = hit.normal;
                    hitEffect.Play();
                    hitSound.Play();
                    Destroy(hitEffect.gameObject, hitEffect.main.duration);
                }
            }
            for (int i = 0; i < tagsToHit.Length; i++)
            {
                if (otherGameObject.CompareTag(tagsToHit[i])) 
                {
                    otherGameObject.SendMessage("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
                }
            }
            Destroy(gameObject);
        }
        parentCollider.enabled = true;
    }

    void OnDestroy() 
    {
        allBullets.Remove(this);
    }

    public static void destroyAllBullets() 
    {
        for (int i = allBullets.Count - 1; i > -1; i--) 
        {
            Destroy(allBullets[i].gameObject);
        }
    }
}
