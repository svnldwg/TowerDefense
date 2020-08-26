using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public float secondsBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;
    private int waveIndex = 0;

    private void OnEnable() {
        SceneManager.sceneLoaded += ResetStatic;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= ResetStatic;
    }

    private void ResetStatic(Scene scene, LoadSceneMode mode)
    {
        enemiesAlive = 0;
    }

    private void Update() {
        Debug.Log(enemiesAlive);
        if (enemiesAlive > 0) 
        {
            return;
        }

        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = secondsBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave() {
        PlayerStats.waves++;

        Wave wave = waves[waveIndex];

        foreach(WaveSection waveSection in wave.waveSections) {
            for(int i = 0; i < waveSection.count; i++) {
                SpawnEnemy(waveSection.enemy);

                yield return new WaitForSeconds(1f / waveSection.rate);
            }
        }

        waveIndex++;

        if (waveIndex == waves.Length) {
            Debug.Log("LEVEL COMPLETE");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }
}
