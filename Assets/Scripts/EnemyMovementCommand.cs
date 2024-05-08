using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementCommand : IUndoableCommand
{
    private Vector3 movementVector;
    private Transform transformToMove;

    private List<Vector3> movementVectors;
    private List<Transform> transformsToMove;
    
    public EnemyMovementCommand(Vector3 movementVector, Transform transform)
    {
        this.movementVector = movementVector;
        transformToMove = transform;
    }

    public EnemyMovementCommand(List<Vector3> movementVectors, List<Transform> transforms)
    {
        this.movementVectors = movementVectors;
        transformsToMove = transforms;
    }
    
    public void Execute()
    {
        //transformToMove.position += movementVector;

        for (int i = 0; i < transformsToMove.Count; i++)
        {
            transformsToMove[i].position += movementVectors[i];
        }
        
    }
    
    public void Undo()
    {
        //transformToMove.position -= movementVector;
        
        for (int i = 0; i < transformsToMove.Count; i++)
        {
            transformsToMove[i].position -= movementVectors[i];
        }
    }
}
