using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int moneyGain = 50;

    public GameObject deathEffect;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.money += moneyGain;
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
