using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class EnemyMovement : ChessPieceMovement
{
    //protected Vector3 VectorToPlayer;
    //protected float DistanceToPlayer;
    
    [SerializeField] protected EnemyData data;

    protected GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Tilemap = FindObjectOfType<Tilemap>();
        CurrentTile = Tilemap.WorldToCell(transform.position);
    }

    private void Update()
    {
        CurrentTile = Tilemap.WorldToCell(transform.position);
    }

    protected Vector3 FindVectorToPlayer(Vector3 vector)
    {
        Vector3 vectorToPlayer = player.transform.position - vector;
        
        return vectorToPlayer;
    }

    protected float FindDistanceToPlayer(Vector3 vector)
    {
        Vector3 vectorToPlayer = player.transform.position - vector;
        float distanceToPlayer = vectorToPlayer.magnitude;
        return distanceToPlayer;
    }

    public virtual Vector3 GetMovementVector()
    {
        throw new NotImplementedException();
    }

    protected virtual Vector3Int FindBestTile()
    {
        return default;
    }
}
