using System.Collections;
using UnityEngine;

namespace Enemy_Movement
{
    public class EnemyRookMovement : EnemyMovement
    {
        public override Vector3 GetMovementVector() 
        {
            var targetTile = FindBestTile(); 
            var centerPos = Tilemap.GetCellCenterWorld(targetTile);
            var movementVector = centerPos - transform.position;
            Debug.Log(movementVector);
            return movementVector;
        }
    
        protected override Vector3Int FindBestTile()
        {
        
            //var targetPos = Tilemap.WorldToCell(rayHit.point);
            var rookMovements = CalculateRookMovements(data.movementRange);
            float minMovement = 1000000;
            int minMovementIndex = 0;
            for (int i = 0; i < rookMovements.Length; i++)
            {
                float distance = FindDistanceToPlayer(rookMovements[i] + new Vector3(0.5f,0.5f));
                //Debug.Log("Movimiento: " + rookMovements[i] + ", distancia: " + distance);
                if (distance < minMovement)
                {
                    minMovement = distance;
                    minMovementIndex = i;
                    //Debug.Log("Nuevo mejor movimiento encontrado: " + rookMovements[i]);
                }
            }

            return rookMovements[minMovementIndex];
        }
    }
}

