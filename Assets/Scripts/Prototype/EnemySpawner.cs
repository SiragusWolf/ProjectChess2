using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Flyweight;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnerData data;
    private int turnsSinceLastSpawn = 0;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        PlayerMovement.onPlayerMoved += TrySpawnEnemies;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerMoved -= TrySpawnEnemies;
    }

    private void TrySpawnEnemies()
    {
        var playerDistance = Vector3.Magnitude(_player.transform.position - transform.position);
        if (playerDistance < data.activationRadius)
        {
            if (turnsSinceLastSpawn >= data.turnsBetweenSpawn)
            {
                Instantiate(GetRandomEnemy(), transform);
                turnsSinceLastSpawn = 0;
            }
            else turnsSinceLastSpawn++;
        }
    }

    private GameObject GetRandomEnemy()
    {
        var randomIndex = Random.Range(0, data.enemyPrefabs.Length);
        GameObject enemy = data.enemyPrefabs[randomIndex];
        
        return enemy;
    }
}
