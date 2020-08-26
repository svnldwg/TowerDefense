using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float initialSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float initialHealth = 100f;
    private float health;
    public int moneyGain = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start() {
        speed = initialSpeed;
        health = initialHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0) {
            Die();
        }

        healthBar.fillAmount = health / initialHealth;
    }

    private void Die()
    {
        PlayerStats.AddMoney(moneyGain);
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.enemiesAlive--;

        Destroy(gameObject);
    }

    public void Slow(float slowAmount)
    {
        speed = initialSpeed * (1f - slowAmount);
    }
}
