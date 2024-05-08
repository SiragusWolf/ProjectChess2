using System;
using System.Collections;
using System.Collections.Generic;
using Facade;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class PlayerMovement : ChessPieceMovement
{
    [SerializeField] private int movementRange;
    private float timeSinceLastMovement;
    [SerializeField] private float movementCooldown;
    private Player _playerComponent;
    [SerializeField] private AudioClip movementSound;
    
    public enum Movement
    {
        Rook,
        Horse,
        Bishop,
        King,
        Queen,
        Pawn
    }
    
    public static event Action onPlayerMoved;

    [SerializeField] private Movement _currentMovement;
    private Movement _nextMovement;
    private Movement _nextNextMovement;

    private INextMoveCanvasProvider _nextMoveCanvasProvider;
    
    private void Awake()
    {
        _nextMoveCanvasProvider = MainCanvas.Instance;
        Tilemap = FindObjectOfType<Tilemap>();
        CurrentTile = Tilemap.WorldToCell(transform.position);

        _currentMovement = RandomMovement();
        _nextMovement = RandomMovement();
        _nextNextMovement = RandomMovement();
        
        _nextMoveCanvasProvider.NextMoveCanvas.UpdateImages(_currentMovement, _nextMovement, _nextNextMovement);
        _playerComponent = GetComponent<Player>();
    }

    void Update()
    {
        CurrentTile = Tilemap.WorldToCell(transform.position);
        timeSinceLastMovement += Time.deltaTime;
    }

    private void OnEnable()
    {
        InputHandler.onClick += MoveToTile;
    }

    private void OnDisable()
    {
        InputHandler.onClick -= MoveToTile;
    }

    private void MoveToTile(RaycastHit2D rayHit)
    {
        var tilePos = Tilemap.WorldToCell(rayHit.point);

        PlayerMovementCommand movementCommand = null;
        bool movementSuccesful = false;

        if (timeSinceLastMovement > movementCooldown && !GameManager.Instance.gameLost)
        {
            switch (_currentMovement)
            {
                case Movement.Horse:
                    var horseMovements = CalculateHorseMovements();
                    for (int i = 0; i < horseMovements.Length; i++)
                    {
                        if (horseMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateHorseMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);

                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }
                    }

                    break;
                case Movement.Bishop:
                    var bishopMovements = CalculateBishopMovements(movementRange);
                    for (int i = 0; i < bishopMovements.Length; i++)
                    {
                        if (bishopMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateBishopMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);
                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }
                    }

                    break;
                case Movement.Rook:
                    var rookMovements = CalculateRookMovements(movementRange);
                    for (int i = 0; i < rookMovements.Length; i++)
                    {
                        if (rookMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateRookMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);

                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }

                        //Debug.Log(rookMovements[i]);
                    }

                    break;
                case Movement.Queen:
                    var queenMovements = CalculateQueenMovements(movementRange);
                    for (int i = 0; i < queenMovements.Length; i++)
                    {
                        if (queenMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateRookMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);
                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }
                    }

                    break;
                case Movement.King:
                    var kingMovements = CalculateKingMovements();
                    for (int i = 0; i < kingMovements.Length; i++)
                    {
                        if (kingMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateRookMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);
                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }
                    }

                    break;
                case Movement.Pawn:
                    var pawnMovements = CalculatePawnMovements();
                    for (int i = 0; i < pawnMovements.Length; i++)
                    {
                        if (pawnMovements[i] == tilePos)
                        {
                            //Debug.Log(CalculateRookMovements()[i]);
                            var centerPos = Tilemap.GetCellCenterWorld(tilePos);
                            var movementVector = centerPos - transform.position;
                            movementCommand = new PlayerMovementCommand(movementVector, transform);
                            movementSuccesful = true;
                        }
                    }

                    break;
            }
        }

        if (movementSuccesful)
        {
            //Hago el audio, cargo la información del comando de movimiento y lo envio
            _playerComponent.SoundEffect(movementSound);
            EventQueue.Instance.EnqueueCommand(movementCommand);
            CurrentTile = Tilemap.WorldToCell(transform.position);
            onPlayerMoved?.Invoke();
            
            //actualizo los proximos movimientos, internamente y en ui
            _currentMovement = _nextMovement;
            _nextMovement = _nextNextMovement;
            _nextNextMovement = RandomMovement();
            _nextMoveCanvasProvider.NextMoveCanvas.UpdateImages(_currentMovement, _nextMovement, _nextNextMovement);
            
            //reinicio el cooldown
            timeSinceLastMovement = 0;
        }
    }

    static Random _R = new Random ();
    static Movement RandomMovement()
    {
        var v = Movement.GetValues(typeof(Movement));
        return (Movement) v.GetValue (_R.Next(v.Length));
    }
}
