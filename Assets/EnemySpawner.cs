using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject swarmerPrefab_2;

    [SerializeField]
    private float swarmerInterval = 3.5f;

     [SerializeField]
    private float bigSwarmerInterval = 10f;

    void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
        StartCoroutine(spawnEnemy(bigSwarmerInterval, swarmerPrefab_2 ));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f,5), Random.Range(-6f,6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
