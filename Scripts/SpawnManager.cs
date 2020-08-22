using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    private GameObject powerupContainer;
    

    private bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startSpawning(){
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playerDead(){
        isOver = true;
    }

    IEnumerator spawnEnemyRoutine(){
        yield return new WaitForSeconds(3.0f);
        while(isOver == false){
            Vector3 posToSpawn = new Vector3(Random.Range(-20f, 20f), 10.0f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator spawnPowerupRoutine(){
        yield return new WaitForSeconds(3.0f);
        while(isOver == false){
            Vector3 posToSpawn = new Vector3(Random.Range(-20f, 20f), 10.0f, 0);
            GameObject newPowerup = Instantiate(powerup[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }
}
