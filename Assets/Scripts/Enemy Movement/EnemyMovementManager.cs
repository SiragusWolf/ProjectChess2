using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    private List<GameObject> enemies;
    private List<GameObject> enemiesToMove = new List<GameObject>();
    private List<Vector3> movementVectors = new List<Vector3>();
    private List<Transform> transformsToMove = new List<Transform>();
    private AudioSource _audioSource;
    [SerializeField]private AudioClip enemyMovementSound;
    private GameObject _player;
    [SerializeField] private int activationRange;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        PlayerMovement.onPlayerMoved += MoveEnemies;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerMoved -= MoveEnemies;
    }

    private void MoveEnemies()
    {
        StartCoroutine(Wait1SecondThenMove());
        
    }

    IEnumerator Wait1SecondThenMove()
    {
        yield return new WaitForSeconds(1);

        var playerPosition = _player.transform.position;
        
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyMovement"));
        
        enemiesToMove.Clear();

        foreach (var enemy in enemies)
        {
            var enemyDistance = Vector3.Magnitude(enemy.transform.position - playerPosition);
            if (enemyDistance < activationRange)
            {
                enemiesToMove.Add(enemy);
            }
        }
        
        movementVectors.Clear();
        transformsToMove.Clear();
        
        foreach (var enemy in enemiesToMove)
        {
            EnemyMovement enemyMovement = enemy.GetComponent(typeof(EnemyMovement)) as EnemyMovement;

            if (enemyMovement != null) movementVectors.Add(enemyMovement.GetMovementVector());
            transformsToMove.Add(enemy.transform);
        }
        var movementCommand = new EnemyMovementCommand(movementVectors, transformsToMove);
        EventQueue.Instance.EnqueueCommand(movementCommand);
        if (enemies.Count != 0)
        {
            _audioSource.PlayOneShot(enemyMovementSound);
        }
    }
}
