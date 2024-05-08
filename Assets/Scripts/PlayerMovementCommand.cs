using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCommand : IUndoableCommand
{
    private Vector3 movementVector;
    private Transform transformToMove;

    public PlayerMovementCommand(Vector3 movementVector, Transform transform)
    {
        this.movementVector = movementVector;
        transformToMove = transform;
    }
    
    public void Execute()
    {
        transformToMove.position += movementVector;
    }
    
    public void Undo()
    {
        transformToMove.position -= movementVector;
    }
}
