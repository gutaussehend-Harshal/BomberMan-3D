using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles enemy spawning logic
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class EnemySpawner : MonoSingletonGeneric<EnemySpawner>
    {
        [Header("Spawning Settings")]
        [SerializeField] private Transform[] spawnLocations;
        [SerializeField] private GameObject[] whatToSpawnPrefab;

        private void Start()
        {
            spawnOfEnemies();
        }

        // This method used for spawning enemies at different locations
        public void spawnOfEnemies()
        {
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                Instantiate(whatToSpawnPrefab[i], spawnLocations[i].transform.position, Quaternion.identity);
            }
        }
    }
}












