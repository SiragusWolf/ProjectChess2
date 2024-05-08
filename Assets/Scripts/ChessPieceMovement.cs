using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ChessPieceMovement : MonoBehaviour
{
    protected Tilemap Tilemap;
    protected Vector3Int CurrentTile;
    protected int MovementRange;

    public Vector3Int GetCurrentTile => CurrentTile;
    
    protected Vector3Int[] CalculateHorseMovements()
    {
        //Se chequean los 8 saltos posibles individualmente
        Vector3Int[] horseMovements = new Vector3Int[8];
        Vector3Int horseM_RUU = new Vector3Int(CurrentTile.x + 1, CurrentTile.y + 2, 0);
        horseMovements[0] = horseM_RUU;
        Vector3Int horseM_RRU = new Vector3Int(CurrentTile.x + 2, CurrentTile.y + 1, 0);
        horseMovements[1] = horseM_RRU;
        Vector3Int horseM_RRD = new Vector3Int(CurrentTile.x + 2, CurrentTile.y - 1, 0);
        horseMovements[2] = horseM_RRD;
        Vector3Int horseM_RDD = new Vector3Int(CurrentTile.x + 1, CurrentTile.y - 2, 0);
        horseMovements[3] = horseM_RDD;
        Vector3Int horseM_LDD = new Vector3Int(CurrentTile.x - 1, CurrentTile.y - 2, 0);
        horseMovements[4] = horseM_LDD;
        Vector3Int horseM_LLD = new Vector3Int(CurrentTile.x - 2, CurrentTile.y - 1, 0);
        horseMovements[5] = horseM_LLD;
        Vector3Int horseM_LLU = new Vector3Int(CurrentTile.x - 2, CurrentTile.y + 1, 0);
        horseMovements[6] = horseM_LLU;
        Vector3Int horseM_LUU = new Vector3Int(CurrentTile.x - 1, CurrentTile.y + 2, 0);
        horseMovements[7] = horseM_LUU;

        return horseMovements;
    }

    protected Vector3Int[] CalculateBishopMovements(int range)
    {
        bool canMoveRU = true; //variables para manejar las direcciones 
        bool canMoveRD = true;
        bool canMoveLU = true;
        bool canMoveLD = true;
        
        Vector3Int[] bishopMovements = new Vector3Int[range * 4];
        for (int i = 0; i < bishopMovements.Length / 4; i++)
        {
            if (canMoveRU) //Chequeo que no haya encontrado un limite antes
            {
                Vector3Int bishopMovement = new Vector3Int(CurrentTile.x + i, CurrentTile.y + i); //Calculo movimiento
                TileBase targetTile = Tilemap.GetTile(bishopMovement); //Busco tile objetivo
                if (targetTile) //Si encuentro tile, se asigna como movimiento posible
                {
                    bishopMovements[4 * i + 0] = bishopMovement;
                }
                else //Si no encuentro tile, no se asigna y se deja de buscar en esa dirección
                {
                    bishopMovements[4 * i + 0] = new Vector3Int(0,0,1000);
                    canMoveRU = false;
                }
            } else bishopMovements[4 * i + 0] = new Vector3Int(0,0, 1000);
            
            if (canMoveRD) //Chequeo que no haya encontrado un limite antes
            {
                Vector3Int bishopMovement = new Vector3Int(CurrentTile.x + i, CurrentTile.y - i); //Calculo movimiento
                TileBase targetTile = Tilemap.GetTile(bishopMovement); //Busco tile objetivo
                if (targetTile) //Si encuentro tile, se asigna como movimiento posible
                {
                    bishopMovements[4 * i + 1] = bishopMovement;
                }
                else //Si no encuentro tile, no se asigna y se deja de buscar en esa dirección
                {
                    bishopMovements[4 * i + 1] = new Vector3Int(0,0,1000);
                    canMoveRD = false;
                }
            } else bishopMovements[4 * i + 1] = new Vector3Int(0,0, 1000);
            
            if (canMoveLU) //Chequeo que no haya encontrado un limite antes
            {
                Vector3Int bishopMovement = new Vector3Int(CurrentTile.x - i, CurrentTile.y + i); //Calculo movimiento
                TileBase targetTile = Tilemap.GetTile(bishopMovement); //Busco tile objetivo
                if (targetTile) //Si encuentro tile, se asigna como movimiento posible
                {
                    bishopMovements[4 * i + 2] = bishopMovement;
                }
                else //Si no encuentro tile, no se asigna y se deja de buscar en esa dirección
                {
                    bishopMovements[4 * i + 2] = new Vector3Int(0, 0, 1000);
                    canMoveLU = false;
                }
            } else bishopMovements[4 * i + 2] = new Vector3Int(0, 0, 1000);
            
            if (canMoveLD) //Chequeo que no haya encontrado un limite antes
            {
                Vector3Int bishopMovement = new Vector3Int(CurrentTile.x - i, CurrentTile.y - i); //Calculo movimiento
                TileBase targetTile = Tilemap.GetTile(bishopMovement); //Busco tile objetivo
                if (targetTile) //Si encuentro tile, se asigna como movimiento posible
                {
                    bishopMovements[4 * i + 3] = bishopMovement;
                }
                else //Si no encuentro tile, no se asigna y se deja de buscar en esa dirección
                {
                    bishopMovements[4 * i + 3] = new Vector3Int(0, 0, 1000);
                    canMoveLD = false;
                }
            } else bishopMovements[4 * i + 3] = new Vector3Int(0, 0, 1000);
        }
        return bishopMovements;
    }

    protected Vector3Int[] CalculateRookMovements(int range)
    {
        bool canMoveR = true; //variables para manejar las direcciones 
        bool canMoveL = true;
        bool canMoveU = true;
        bool canMoveD = true;
        
        Vector3Int[] rookMovements = new Vector3Int[range * 4];
        for (int i = 0; i < rookMovements.Length / 4; i++)
        {
            if (canMoveR) //Chequeo que no haya encontrado un limite antes
            {
                Vector3Int rookMovement = new Vector3Int(CurrentTile.x + i, CurrentTile.y); //Calculo movimiento
                TileBase targetTile = Tilemap.GetTile(rookMovement); //Busco tile objetivo
                if (targetTile) //Si encuentro tile, se asigna como movimiento posible
                {
                    rookMovements[4 * i + 0] = rookMovement;
                }
                else //Si no encuentro tile, no se asigna y se deja de buscar en esa dirección
                {
                    rookMovements[4 * i + 0] = new Vector3Int(0, 0, 1000);
                    canMoveR = false;
                }
            } else rookMovements[4 * i + 0] = new Vector3Int(0, 0, 1000);

            if (canMoveL)
            {
                Vector3Int rookMovement = new Vector3Int(CurrentTile.x - i, CurrentTile.y);
                TileBase targetTile = Tilemap.GetTile(rookMovement);
                if (targetTile)
                {
                    rookMovements[4 * i + 1] = rookMovement;
                }
                else
                {
                    rookMovements[4 * i + 1] = new Vector3Int(0, 0, 1000);
                    canMoveL = false;
                }
            } else rookMovements[4 * i + 1] = new Vector3Int(0, 0, 1000);
            
            if (canMoveU)
            {
                Vector3Int rookMovement = new Vector3Int(CurrentTile.x, CurrentTile.y + i);
                TileBase targetTile = Tilemap.GetTile(rookMovement);
                if (targetTile)
                {
                    rookMovements[4 * i + 2] = rookMovement;
                }
                else
                {
                    rookMovements[4 * i + 2] = new Vector3Int(0, 0, 1000);
                    canMoveU = false;
                }
            } else rookMovements[4 * i + 2] = new Vector3Int(0, 0, 1000);
            
            if (canMoveD)
            {
                Vector3Int rookMovement = new Vector3Int(CurrentTile.x, CurrentTile.y - i);
                TileBase targetTile = Tilemap.GetTile(rookMovement);
                if (targetTile)
                {
                    rookMovements[4 * i + 3] = rookMovement;
                }
                else
                {
                    rookMovements[4 * i + 3] = new Vector3Int(0, 0, 1000);
                    canMoveD = false;
                }
            } else rookMovements[4 * i + 3] = new Vector3Int(0, 0, 1000);
        }
        return rookMovements;
    }

    protected Vector3Int[] CalculateQueenMovements(int range)
    {
        //Se combinan los chequeos de torre y alfil
        Vector3Int[] rookMovements = CalculateRookMovements(range);
        Vector3Int[] bishopMovements = CalculateBishopMovements(range);
        Vector3Int[] queenMovements =
            new Vector3Int[rookMovements.Length + bishopMovements.Length];
        for (int i = 0; i < rookMovements.Length; i++)
        {
            queenMovements[i] = rookMovements[i];
        }
        for (int i = 0; i < bishopMovements.Length; i++)
        {
            queenMovements[rookMovements.Length + i] = bishopMovements[i];
        }
        return queenMovements;
    }

    protected Vector3Int[] CalculateKingMovements()
    {
        //Se chequean los 8 movimientos posibles individualmente
        Vector3Int[] kingMovements = new Vector3Int[8];
        Vector3Int kingM_U = new Vector3Int(CurrentTile.x, CurrentTile.y + 1);
        kingMovements[0] = kingM_U;
        Vector3Int kingM_RU = new Vector3Int(CurrentTile.x + 1, CurrentTile.y + 1);
        kingMovements[1] = kingM_RU;
        Vector3Int kingM_R = new Vector3Int(CurrentTile.x + 1, CurrentTile.y);
        kingMovements[2] = kingM_R;
        Vector3Int kingM_RD = new Vector3Int(CurrentTile.x + 1, CurrentTile.y - 1);
        kingMovements[3] = kingM_RD;
        Vector3Int kingM_D = new Vector3Int(CurrentTile.x, CurrentTile.y - 1);
        kingMovements[4] = kingM_D;
        Vector3Int kingM_LD = new Vector3Int(CurrentTile.x - 1, CurrentTile.y - 1);
        kingMovements[5] = kingM_LD;
        Vector3Int kingM_L = new Vector3Int(CurrentTile.x - 1, CurrentTile.y);
        kingMovements[6] = kingM_L;
        Vector3Int kingM_LU = new Vector3Int(CurrentTile.x - 1, CurrentTile.y + 1);
        kingMovements[7] = kingM_LU;

        return kingMovements;
    }

    protected Vector3Int[] CalculatePawnMovements()
    {
        //Se chequean los 4 movimientos posibles individualmente
        Vector3Int[] pawnMovements = new Vector3Int[4];
        Vector3Int kingM_U = new Vector3Int(CurrentTile.x, CurrentTile.y + 1);
        pawnMovements[0] = kingM_U;
        Vector3Int kingM_R = new Vector3Int(CurrentTile.x + 1, CurrentTile.y);
        pawnMovements[1] = kingM_R;
        Vector3Int kingM_D = new Vector3Int(CurrentTile.x, CurrentTile.y - 1);
        pawnMovements[2] = kingM_D;
        Vector3Int kingM_L = new Vector3Int(CurrentTile.x - 1, CurrentTile.y);
        pawnMovements[3] = kingM_L;

        return pawnMovements;
    }
}
