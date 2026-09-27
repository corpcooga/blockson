using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    bool canTakeDamage = true;
    int maxHealth;
    Quaternion startRotation;
    Vector3 startPosition;
    Rigidbody rb;
    PlayerControls playerControls;
    [Header("Health")]
    public int health = 3;
    public Image[] hearts;
    [Header("Obstacle Handling")]
    public float pushDistance = 0.1f;
    public float hitCooldown = 1;
    [Header("Level")]
    public static int level = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = GetComponent<PlayerControls>();
        maxHealth = health;
        startRotation = transform.rotation;
        startPosition = transform.position;
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < health;
        }
        if (transform.position.y < -10)
        {
            killPlayer();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Lava"))
        {
            killPlayer();
        }
        if (collision.collider.gameObject.CompareTag("Spike") || collision.collider.gameObject.CompareTag("Enemy"))
        {
            takeDamage();
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 pushDirection = transform.position - contactPoint;
            pushDirection.Normalize();
            rb.MovePosition(rb.position + pushDirection * pushDistance);
        }
        if (collision.collider.gameObject.CompareTag("End Goal"))
        {
            FindObjectOfType<AudioManager>().Play("Level Finish Sound");
            nextLevel();
        }
    }

    public void takeDamage(int damage = 1)
    {
        if (canTakeDamage)
        {
            health -= damage;
            StartCoroutine(damageCooldown());
            if (health >= 1) FindObjectOfType<AudioManager>().Play("Hurt Sound");
            if (health <= 0) killPlayer();
        }
    }

    IEnumerator damageCooldown() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(hitCooldown);
        canTakeDamage = true;
    }

    public void killPlayer()
    {
        FindObjectOfType<AudioManager>().Play("Death Sound");
        respawn(startPosition, startRotation);
    }

    public void heal(int healAmount = 1)
    {
        health += healAmount;
        FindObjectOfType<AudioManager>().Play("Heal Sound");
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void fullHeal()
    {
        health = maxHealth;
    }

    public void respawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        playerControls.resetRotation();
        EnemyHealth.respawnAll();
        HealthKit.respawnAll();
        GenericMovement.resetAllGeneric();
        ShootingEnemy.resetAllShootingEnemies();
        BulletScript.destroyAllBullets();
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
        fullHeal();
        StartCoroutine(damageCooldown());
    }

    public void respawn(Vector3 position, Vector3 EulerAngles)
    {
        respawn(position, Quaternion.Euler(EulerAngles));
    }

    public void nextLevel()
    {
        PauseMenu.instance.winUI.SetActive(true);
        PauseMenu.GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        level++;
    }
}
