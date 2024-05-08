using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemy_Movement
{
    public class EnemyKingMovement : EnemyMovement
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
            var kingMovements = CalculateKingMovements();
            float minMovement = 1000000;
            int minMovementIndex = 0;
            for (int i = 0; i < kingMovements.Length; i++)
            {
                var FindTile = Tilemap.GetTile(kingMovements[i]);
                if (FindTile)
                {
                    float distance = FindDistanceToPlayer(kingMovements[i] + new Vector3(0.5f, 0.5f));
                    //Debug.Log("Movimiento: " + horseMovements[i] + ", distancia: " + distance);
                    if (distance < minMovement)
                    {
                        minMovement = distance;
                        minMovementIndex = i;
                        //Debug.Log("Nuevo mejor movimiento encontrado: " + horseMovements[i]);
                    }
                }
            }

            return kingMovements[minMovementIndex];
        }
    }
}
