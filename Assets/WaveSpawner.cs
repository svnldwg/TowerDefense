using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float secondsBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;
    private int waveIndex = 0;

    private void Update() {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = secondsBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    private IEnumerator SpawnWave() {
        waveIndex++;

        for(int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
