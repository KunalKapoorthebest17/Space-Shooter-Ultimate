﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRoutine()                    //Creating coroutine
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
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
