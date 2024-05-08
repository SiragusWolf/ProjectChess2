using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private Tilemap tilemap;
    
    //public delegate void OnClick(RaycastHit2D raycastHit2D);
    //public static event OnClick onClick;

    public static event Action<RaycastHit2D> onClick;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        tilemap = FindObjectOfType<Tilemap>();
    }
    
    public void ManageClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        //Debug.Log(tilemap.WorldToCell(rayHit.point));
        onClick?.Invoke(rayHit);
        
        
        //Gets the point hit then move into the block a small amount to ensure it doesn't round and gets the correct gridspace
        /*var hitTile = tilemap.GetTile(tilemap.WorldToCell(new Vector2(rayHit.point.x - (rayHit.normal.x * 0.01f), rayHit.point.y - (rayHit.normal.y * 0.01f))));

        if (hitTile)
        {
            Debug.Log(hitTile.name);
            onClick?.Invoke(hitTile);
        }*/
    }
}