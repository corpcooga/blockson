using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    int maxHealth;
    Quaternion startRotation;
    Vector3 startPosition;

    public ParticleSystem hitEffect;
    Vector3 hitEffectPosition;
    Vector3 hitEffectRotation;
    Transform hitEffectParent;

    public AudioSource hitHurtSound;
    public AudioSource hitDeathSound;

    static List<EnemyHealth> allEnemies = new List<EnemyHealth>();

    void Start()
    {
        hitEffectPosition = hitEffect.transform.localPosition;
        hitEffectRotation = hitEffect.transform.localEulerAngles;
        hitEffectParent = hitEffect.transform.parent;
        allEnemies.Add(this);
        maxHealth = health;
        startRotation = transform.rotation;
        startPosition = transform.position;
    }

    void OnDestroy()
    {
        allEnemies.Remove(this);
    }

    public static void respawnAll()
    {
        for (int i = 0; i < allEnemies.Count; i++)
        {
            allEnemies[i].respawn();
        }
    }

    public void takeDamage(int damage = 1)
    {
        health -= damage;
        hitEffect.Play();
        if (health >= 1) hitHurtSound.Play();
        if (health <= 0) killEnemy();
    }

    public void killEnemy()
    {
        hitDeathSound.Play();
        hitEffect.transform.parent = transform.parent;
        gameObject.SetActive(false);
    }

    public void fullHeal()
    {
        health = maxHealth;
    }

    public void respawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        hitEffect.transform.parent = hitEffectParent;
        hitEffect.transform.localPosition = hitEffectPosition;
        hitEffect.transform.localEulerAngles = hitEffectRotation;
        fullHeal();
        gameObject.SetActive(true);
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null) 
        {
            enemyMovement.ResetDirection();
        }
    }

    public void respawn(Vector3 position, Vector3 EulerAngles)
    {
        respawn(position, Quaternion.Euler(EulerAngles));
    }

    public void respawn()
    {
        respawn(startPosition, startRotation);
    }
}
