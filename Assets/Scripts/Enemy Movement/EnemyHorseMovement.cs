using System.Collections;
using UnityEngine;

namespace Enemy_Movement
{
    public class EnemyHorseMovement : EnemyMovement
    {
        /*private void OnEnable()
        {
            InputHandler.onClick += MoveToTile;
        }

        private void OnDisable()
        {
            InputHandler.onClick -= MoveToTile;
        }*/

        /*private void MoveToTile(RaycastHit2D rayHit)
        {
            StartCoroutine(Wait1SecondThenMove(rayHit));
        }

        IEnumerator Wait1SecondThenMove(RaycastHit2D rayHit)
        {
            yield return new WaitForSeconds(1);

            var targetTile = FindBestTile();
            var centerPos = Tilemap.GetCellCenterWorld(targetTile);
            var movementVector = centerPos - transform.position;
            var movementCommand = new EnemyMovementCommand(movementVector, transform);
            EventQueue.Instance.EnqueueCommand(movementCommand);
        }*/
        public override Vector3 GetMovementVector() 
        {
            var targetTile = FindBestTile(); 
            var centerPos = Tilemap.GetCellCenterWorld(targetTile);
            
            //Collider2D[] hitColliders = Physics2D.OverlapPointAll(centerPos);
            //Debug.Log(Tilemap.GetCellCenterWorld(horseMovements[i])); 
            //Debug.Log(hitColliders.Length);
            //Debug.Log(movementVector);
            
            var movementVector = centerPos - transform.position;
            return movementVector;
        }

        protected override Vector3Int FindBestTile()
        {
            //var targetPos = Tilemap.WorldToCell(rayHit.point);
            var horseMovements = CalculateHorseMovements();
            float minMovement = 1000000;
            int minMovementIndex = 0;
            for (int i = 0; i < horseMovements.Length; i++)
            {
                var FindTile = Tilemap.GetTile(horseMovements[i]);
                // Collider2D[] hitColliders = Physics2D.OverlapPointAll(Tilemap.GetCellCenterWorld(horseMovements[i]));
                // Debug.Log(Tilemap.GetCellCenterWorld(horseMovements[i]));
                // Debug.Log(hitColliders.Length);
                if (FindTile)
                {
                    float distance = FindDistanceToPlayer(horseMovements[i] + new Vector3(0.5f, 0.5f));
                    //Debug.Log("Movimiento: " + horseMovements[i] + ", distancia: " + distance);
                    if (distance < minMovement)
                    {
                        minMovement = distance;
                        minMovementIndex = i;
                        //Debug.Log("Nuevo mejor movimiento encontrado: " + horseMovements[i]);
                    }
                }
            }
            return horseMovements[minMovementIndex];
        }
    }
}
