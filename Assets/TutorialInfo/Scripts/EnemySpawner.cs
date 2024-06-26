using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyPos;

    private bool canSpawn;


    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine("SpawnEnemy");
        }
        
    }
    IEnumerator SpawnEnemy()
    {
        
        Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
        Debug.Log("Enemy position: " + enemyPos.transform.position);
        canSpawn = false;
        yield return new WaitForSeconds(4f);
        canSpawn = true;
    }
}
