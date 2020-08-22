using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float initialSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100f;
    public int moneyGain = 50;

    public GameObject deathEffect;

    private void Start() {
        speed = initialSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.AddMoney(moneyGain);
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    public void Slow(float slowAmount)
    {
        speed = initialSpeed * (1f - slowAmount);
    }
}
