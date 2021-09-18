using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemySpawner : MonoSingletonGeneric<EnemySpawner>
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject[] whatToSpawnPrefab;

    private void Start()
    {
        spawnOfEnemies();
    }

    // public int EnemyCount()
    // {
    //     return  whatToSpawnPrefab.Length;
    // }

    public void spawnOfEnemies()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Instantiate(whatToSpawnPrefab[i], spawnLocations[i].transform.position, Quaternion.identity);
        }
    }
}
