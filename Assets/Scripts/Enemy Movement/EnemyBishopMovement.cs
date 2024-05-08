using System.Collections;
using UnityEngine;

namespace Enemy_Movement
{
    public class EnemyBishopMovement : EnemyMovement
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
            var bishopMovements = CalculateBishopMovements(data.movementRange);
            float minMovement = 1000000;
            int minMovementIndex = 0;
            //Collider2D[] collisions = Physics2D.
            for (int i = 0; i < bishopMovements.Length; i++)
            {
                float distance = FindDistanceToPlayer(bishopMovements[i] + new Vector3(0.5f,0.5f));
                //Debug.Log("Movimiento: " + bishopMovements[i] + ", distancia: " + distance);
                if (distance < minMovement)
                {
                    minMovement = distance;
                    minMovementIndex = i;
                    //Debug.Log("Nuevo mejor movimiento encontrado: " + bishopMovements[i]);
                }
            }
            return bishopMovements[minMovementIndex];
        }
    }
}
