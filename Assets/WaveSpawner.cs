using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
 public GameObject enemyPrefab; // Reference to the enemy prefab to spawn.
    public int numberOfEnemies = 5; // Number of enemies to spawn in each wave.
    public float timeBetweenWaves = 10f; // Time between each wave.

    public GameOverScreen gameOverScreen;

    private void Start() {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves() {
        Debug.Log("Game start");
       while (!gameOverScreen.gameOver) {
            for (int i = 0; i < numberOfEnemies; i++) {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                Destroy(enemy, 10f);
                //yield return new WaitForSeconds(1f); // Delay between spawning enemies in a wave.
            }
            
            yield return new WaitForSeconds(10f); // Delay between waves.
       }
    }
}
