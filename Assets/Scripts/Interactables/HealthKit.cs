using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public int healAmount = 1;
    static List<HealthKit> allHealthKits = new List<HealthKit>();

    void Start()
    {
        allHealthKits.Add(this);
    }

    void OnDestroy() 
    {
        allHealthKits.Remove(this);
    }

    public void collectKit()
    {
        gameObject.SetActive(false);
    }

    public static void respawnAll() 
    {
        for (int i = 0; i < allHealthKits.Count; i++) 
        {
            allHealthKits[i].respawn();
        }
    }

    public void respawn() 
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other) 
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.heal(healAmount);
            collectKit();
        }
    }
}
