using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    public Light impactLight;
    public int initialLaserDamagePerSecond = 20;
    public int laserDamageIncreasePercentPerSecond = 50;
    private int currentLaserDamagePerSecond;
    
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float rotationSpeed = 10f;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        currentLaserDamagePerSecond = initialLaserDamagePerSecond;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        InvokeRepeating("LaserDamage", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target == null) {
            if (useLaser && lineRenderer.enabled) {
                lineRenderer.enabled = false;
                laserImpactEffect.Stop();
                impactLight.enabled = false;
            }
            return;
        }

        LockOnTarget();

        if (useLaser) {
            Laser();
        } else {
            if (fireCountdown <= 0f) {
                Shoot();
                fireCountdown = 1f/fireRate;
            }
        }
    }

    private void Laser()
    {
        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dirFromTargetToTurret = firePoint.position - target.position;
        laserImpactEffect.transform.position = target.position + dirFromTargetToTurret.normalized * (target.transform.localScale.y / 2);
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dirFromTargetToTurret);
    }

    private void LaserDamage()
    {
        if (!useLaser || target == null || !lineRenderer.enabled) {
            currentLaserDamagePerSecond = initialLaserDamagePerSecond;

            return;
        }

        EnemyHealth eh = target.GetComponent<EnemyHealth>();
        if (eh != null) {
            Debug.Log("Causing Damage of " + (currentLaserDamagePerSecond / 2f));
            eh.TakeDamage(Mathf.FloorToInt(currentLaserDamagePerSecond / 2f));
            float laserDamageIncreaseFactor = 1 + laserDamageIncreasePercentPerSecond / 2f / 100f;
            currentLaserDamagePerSecond = Mathf.FloorToInt(currentLaserDamagePerSecond * laserDamageIncreaseFactor);
        }
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation =  Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
