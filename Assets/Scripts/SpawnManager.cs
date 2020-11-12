using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] powerups;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        //yield return null //Wait 1 frame

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSecondsRealtime(5.0f);
        }

    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 7f, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newEnemy = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            int wait_time = Random.Range(5, 10);

            yield return new WaitForSeconds(wait_time);
        }


    }




    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        Destroy(this.gameObject);
    }


}
