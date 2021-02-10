using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    void Start()
    {
        
    }

    public void DecreasePlayerHealth(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            print("Player Dead!");

        }
    }
}
